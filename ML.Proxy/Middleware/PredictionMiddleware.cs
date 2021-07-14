using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using ML.Proxy.DataModels;
using ML.Proxy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public PredictionMiddleware(
            ILogger<PredictionMiddleware> logger,
            RequestDelegate next,
            PredictionEnginePool<GoldenEyeTrafficData, NetworkAttackPrediction> goldenEyeModel,
            PredictionEnginePool<LOICTrafficData, NetworkAttackPrediction> loicModel,
            PredictionEnginePool<SlowlorisTrafficData, NetworkAttackPrediction> slowlorisModel,
            IRequestProcessingService transformationService,
            ICaptureTrafficService captureService
            ) 
        {
            _logger = logger;
            _next = next;
            _goldenEyeModel = goldenEyeModel;
            _loicModel = loicModel;
            _slowlorisModel = slowlorisModel;
            _transformationService = transformationService;
            _captureService = captureService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _captureService.CaptureTraffic();

            // Setzen eines Headers, der den eingehenden Request als möglichen Angriff kennzeichnet
            // Bei true: Handelt es sich um einen Angriff
            // Bei false: Wird von Seiten des ML.Proxy nichts unternommen
            context.Request.Headers.Add("Attack-Prediction-Header", "false");
            
            await _next.Invoke(context);
        }
    }
}
