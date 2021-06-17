using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Proxy.ModelTrainer.Models
{
    public class NetworkTrafficData
    {
        // Template for addressing the four selected scopes out of the paper
        [LoadColumn(0)]
        public string IpAdress;
    }

    public class NetworkTrafficDataDoSPrediction
    {
        // The Prediction consists of: 
        // - an alert to indicate whether there is an anomaly
        // - a raw score
        // - p-value (the closer this one is to 0, the more likely an anomaly has occured)
        [VectorType(3)]
        public double[] Prediction { get; set; }
    }
}
