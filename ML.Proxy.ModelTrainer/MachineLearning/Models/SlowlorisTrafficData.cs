using Microsoft.ML.Data;

namespace ML.Proxy.ModelTrainer.MachineLearning.Models
{
    /// <summary>
    /// Data Model for Representing Slowloris Network Traffic
    /// </summary>
    public class SlowlorisTrafficData
    {
        [LoadColumn(0)]
        public float FlowDuration;
        [LoadColumn(1)]
        public float BwdIATMean;
        [LoadColumn(2)]
        public float FwdIATMin;
        [LoadColumn(3)]
        public float FwdIATMean;
        [LoadColumn(4)]
        public bool Label;
    }
}
