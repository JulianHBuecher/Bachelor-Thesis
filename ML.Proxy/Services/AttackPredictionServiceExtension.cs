using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ML;
using ML.Proxy.DataModels;
using System;

namespace ML.Proxy.Services
{
    public static class AttackPredictionServiceExtension
    {
        public static void AddMachineLearningAttackPrediction(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPredictionEnginePool<GoldenEyeTrafficData, NetworkAttackPrediction>()
                .FromUri(
                    modelName: "GoldenEyeAttackModel",
                    uri: configuration.GetValue<string>("ML.Proxy.ML-Modell:ZIP:GoldenEye:Model-Path"),
                    period: TimeSpan.FromMinutes(10));
            services.AddPredictionEnginePool<LOICTrafficData, NetworkAttackPrediction>()
                .FromUri(
                    modelName: "LOICAttackModel",
                    uri: configuration.GetValue<string>("ML.Proxy.ML-Modell:ZIP:LOIC:Model-Path"),
                    period: TimeSpan.FromMinutes(10));
            services.AddPredictionEnginePool<SlowlorisTrafficData, NetworkAttackPrediction>()
                .FromUri(
                    modelName: "SlowlorisAttackModel",
                    uri: configuration.GetValue<string>("ML.Proxy.ML-Modell:ZIP:Slowloris:Model-Path"),
                    period: TimeSpan.FromMinutes(10));

            services.TryAddSingleton<IRequestProcessingService, RequestProcessingService>();
            services.TryAddSingleton<ICaptureTrafficService, CaptureTrafficService>();
            services.TryAddSingleton<IRedisCacheService, RedisCacheService>();
            services.TryAddSingleton<IIPBlacklistService, IPBlacklistService>();
        }
    }
}
