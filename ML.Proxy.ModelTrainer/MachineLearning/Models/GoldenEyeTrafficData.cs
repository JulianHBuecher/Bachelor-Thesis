using Microsoft.ML.Data;

namespace ML.Proxy.ModelTrainer.Models
{
    /// <summary>
    /// Data Model for representing GoldenEye Network Traffic
    /// </summary>
    public class GoldenEyeTrafficData
    {
        [LoadColumn(0)]
        public float BwdPktLenStd;
        [LoadColumn(1)]
        public float FlowIATMin;
        [LoadColumn(2)]
        public float FwdIATMin;
        [LoadColumn(3)]
        public float FlowIATMean;
        [LoadColumn(4)]
        public bool Label;

    }
}
