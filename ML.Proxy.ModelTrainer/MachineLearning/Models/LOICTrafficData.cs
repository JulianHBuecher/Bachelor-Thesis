using Microsoft.ML.Data;

namespace ML.Proxy.ModelTrainer.Models
{
    /// <summary>
    /// Data Model for Representing LOIC Network Traffic
    /// </summary>
    public class LOICTrafficData
    {
        [LoadColumn(0)]
        public float BwdPktLenStd;
        [LoadColumn(1)]
        public float PktSizeAvg;
        [LoadColumn(2)]
        public float FlowDuration;
        [LoadColumn(3)]
        public float FlowIATStd;
        [LoadColumn(4)]
        public bool Label;
    }
}
