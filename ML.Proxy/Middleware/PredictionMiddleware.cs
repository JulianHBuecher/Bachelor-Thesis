using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using ML.Proxy.DataModels;
using ML.Proxy.Services;
using System;
using System.Threading.Tasks;

namespace ML.Proxy.Middleware
{
    public class PredictionMiddleware
    {
        private readonly ILogger<PredictionMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly PredictionEnginePool<GoldenEyeTrafficData, NetworkAttackPrediction> _goldenEyeModel;
        private readonly PredictionEnginePool<LOICTrafficData, NetworkAttackPrediction> _loicModel;
        private readonly PredictionEnginePool<SlowlorisTrafficData, NetworkAttackPrediction> _slowlorisModel;
        private readonly IRequestProcessingService _transformationService;
        private readonly ICaptureTrafficService _captureService;
        private readonly IIPBlocklistService _blocklistService;
        private readonly IIPSafelistService _safelistService;
        private readonly IPacketService _packetService;


        public PredictionMiddleware(
            ILogger<PredictionMiddleware> logger,
            RequestDelegate next,
            PredictionEnginePool<GoldenEyeTrafficData, NetworkAttackPrediction> goldenEyeModel,
            PredictionEnginePool<LOICTrafficData, NetworkAttackPrediction> loicModel,
            PredictionEnginePool<SlowlorisTrafficData, NetworkAttackPrediction> slowlorisModel,
            IRequestProcessingService transformationService,
            ICaptureTrafficService captureService,
            IIPBlocklistService blocklistService,
            IIPSafelistService safelistService,
            IPacketService packetService
            ) 
        {
            _logger = logger;
            _next = next;
            _goldenEyeModel = goldenEyeModel;
            _loicModel = loicModel;
            _slowlorisModel = slowlorisModel;
            _transformationService = transformationService;
            _captureService = captureService;
            _blocklistService = blocklistService;
            _safelistService = safelistService;
            _packetService = packetService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            DateTime initialPacketTimestamp, timestamp;
            initialPacketTimestamp = timestamp = DateTime.Now;

            var packet = _captureService.CaptureTraffic();

            if (packet is null)
            {
                _logger.LogWarning("No packet is available!");
                await _next.Invoke(context);
                return;
            }
            else
            {
                var isOnSafelist = await _safelistService.IsOnSafelist(context);
                
                if (isOnSafelist)
                {
                    await _next.Invoke(context);
                    return;
                }
                
                var potentialAttacker = await _blocklistService.IsIPBlocklisted(context);

                if (potentialAttacker.IsBlocklisted)
                {
                    AddPredictedAttackHeader(context, true);
                    await _next.Invoke(context);
                    return;
                }

                try
                {
                    (var firstPacket, var lastPacket) = await _packetService.GetInitialAndLastPacket(context);

                    if (lastPacket is not null && firstPacket is not null)
                    {
                        timestamp = lastPacket.Timeval.Date;
                        initialPacketTimestamp = firstPacket.Timeval.Date;

                        await _packetService.AddNewPacket(packet, context);
                    }
                    else
                    {
                        // Ist kein letztes Paket im Cache, füge ein initiales Paket hinzu
                        await _packetService.AddInitialPacket(packet, context);
                    }
                }
                catch(Exception e)
                {
                    _logger.LogError($"{e}");
                }

                var (goldenEyeAttack, loicAttack, slowlorisAttack) = _transformationService.Transform(initialPacketTimestamp,timestamp,packet);

                _logger.LogInformation($"{goldenEyeAttack}\n{loicAttack}\n{slowlorisAttack}");

                var attackPrediction = PredictAttack(_goldenEyeModel, _loicModel, _slowlorisModel, goldenEyeAttack, loicAttack, slowlorisAttack);

                if (attackPrediction.IsGoldenEyeAttack || attackPrediction.IsLOICAttack || attackPrediction.IsSlowlorisAttack)
                {
                    await _blocklistService.BlocklistIP(context);
                    // Setzen eines Headers, der den eingehenden Request als möglichen Angriff kennzeichnet
                    // Bei true: Handelt es sich um einen Angriff
                    _logger.LogWarning($"Attack predicted: Votage in Consortium:\nGoldenEye: {attackPrediction.IsGoldenEyeAttack}; LOIC: {attackPrediction.IsLOICAttack}; Slowloris: {attackPrediction.IsSlowlorisAttack}");
                    AddPredictedAttackHeader(context, true);
                }
                else
                {
                    // Bei false: Wird von Seiten des ML.Proxy nichts unternommen
                    _logger.LogInformation($"Request is not an attack.");
                    AddPredictedAttackHeader(context, false);
                }

                await _next.Invoke(context);
            }
        }

        private static (bool IsGoldenEyeAttack, bool IsLOICAttack, bool IsSlowlorisAttack) PredictAttack(
            PredictionEnginePool<GoldenEyeTrafficData, NetworkAttackPrediction> goldenEyeModel,
            PredictionEnginePool<LOICTrafficData, NetworkAttackPrediction> loicModel,
            PredictionEnginePool<SlowlorisTrafficData, NetworkAttackPrediction> slowlorisModel,
            GoldenEyeTrafficData goldenEyeAttack, 
            LOICTrafficData loicAttack, 
            SlowlorisTrafficData slowlorisAttack)
        {
            var goldenEyePrediction = goldenEyeModel.Predict(modelName: "GoldenEyeAttackModel", example: goldenEyeAttack);
            var loicPrediction = loicModel.Predict(modelName: "LOICAttackModel", example: loicAttack);
            var slowlorisPrediction = slowlorisModel.Predict(modelName: "SlowlorisAttackModel", example: slowlorisAttack);

            return (goldenEyePrediction.PredictedLabel, loicPrediction.PredictedLabel, slowlorisPrediction.PredictedLabel);
        }

        public static void AddPredictedAttackHeader(HttpContext context, bool IsAttack)
        {
            var attackHeader = "Attack-Prediction-Header";
            var attackHeaderAlreadyExists = context.Request.Headers.ContainsKey(attackHeader);

            switch (IsAttack, attackHeaderAlreadyExists)
            {
                case (true, false):
                    context.Request.Headers.Add(attackHeader, "true");
                    break;
                case (false, false):
                    context.Request.Headers.Add(attackHeader, "false");
                    break;
                default:
                    // Header muss nicht hinzugefügt werden
                    break;
            }
        }
    }
}
