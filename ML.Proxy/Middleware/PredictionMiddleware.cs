using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using ML.Proxy.DataModels;
using ML.Proxy.Models;
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
        private readonly IRedisCacheService _cache;


        public PredictionMiddleware(
            ILogger<PredictionMiddleware> logger,
            RequestDelegate next,
            PredictionEnginePool<GoldenEyeTrafficData, NetworkAttackPrediction> goldenEyeModel,
            PredictionEnginePool<LOICTrafficData, NetworkAttackPrediction> loicModel,
            PredictionEnginePool<SlowlorisTrafficData, NetworkAttackPrediction> slowlorisModel,
            IRequestProcessingService transformationService,
            ICaptureTrafficService captureService,
            IRedisCacheService cache
            ) 
        {
            _logger = logger;
            _next = next;
            _goldenEyeModel = goldenEyeModel;
            _loicModel = loicModel;
            _slowlorisModel = slowlorisModel;
            _transformationService = transformationService;
            _captureService = captureService;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cacheKey = "Last-Packet";
            var initialCacheKey = "First-Packet";
            var timestamp = DateTime.Now;
            var initialPacketTimestamp = DateTime.Now;

            var packet = _captureService.CaptureTraffic();

            if (packet is null)
            {
                _logger.LogWarning($"No packet is available!");
                await _next.Invoke(context);
            }
            else
            {
                try
                {
                    var firstPacket = await _cache.GetAsync<RawPacketCapture>(initialCacheKey);
                    var lastPacket = await _cache.GetAsync<RawPacketCapture>(cacheKey);
                    if (lastPacket is not null)
                    {
                        timestamp = lastPacket.Timeval.Date;
                        initialPacketTimestamp = firstPacket.Timeval.Date;
                        // Hinzufügen des alten letzten Paketes für die Historie
                        await _cache.UpdateAsync(cacheKey, timestamp.ToString());
                        // Setzen eines neuen letzten Paketes für die Zeitstempel
                        await _cache.SetAsync(cacheKey, packet);
                    }
                    else
                    {
                        // Ist kein letztes Paket im Cache, füge ein initiales Paket hinzu
                        await _cache.SetAsync(initialCacheKey, packet);
                        await _cache.SetAsync(cacheKey, packet);
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
                    // Setzen eines Headers, der den eingehenden Request als möglichen Angriff kennzeichnet
                    // Bei true: Handelt es sich um einen Angriff
                    _logger.LogWarning($"Attack predicted: Votage in Consortium:\nGoldenEye: {attackPrediction.IsGoldenEyeAttack}; LOIC: {attackPrediction.IsLOICAttack}; Slowloris: {attackPrediction.IsSlowlorisAttack}");
                    context.Request.Headers.Add("Attack-Prediction-Header", "true");
                }
                else
                {
                    // Bei false: Wird von Seiten des ML.Proxy nichts unternommen
                    _logger.LogInformation("Request is not an attack.");
                    context.Request.Headers.Add("Attack-Prediction-Header", "false");
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
    }
}
