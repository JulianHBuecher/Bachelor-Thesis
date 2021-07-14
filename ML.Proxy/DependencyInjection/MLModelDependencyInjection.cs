using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Proxy.DependencyInjection
{
    public static class MLModelDependencyInjection
    {
        public static void AddPredictionEnginePoolFromUri(this IServiceCollection services, string modelName, string uri, TimeSpan refreshTime)
        {
            
        }
    }
}
