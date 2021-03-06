using Microsoft.ML.Data;

namespace ML.Proxy.DataModels
{
    /// <summary>
    /// Data Model for Representing LOIC Network Traffic
    /// </summary>
    public class LOICTrafficData
    {
        [LoadColumn(1)]
        public float PktSizeAvg;
        [LoadColumn(2)]
        public float FlowDuration;
        [LoadColumn(3)]
        public float FlowIATStd;
        [LoadColumn(4)]
        public bool Label;

        public LOICTrafficData() { }

        public LOICTrafficData(NetworkAttack attack)
        {
            PktSizeAvg = attack.PktSizeAvg;
            FlowDuration = attack.FlowDuration;
            FlowIATStd = attack.FlowIATStd;
        }

        public override string ToString()
        {
            return $"Metrics for LOICTraffic: PktSizeAvg: {PktSizeAvg}, FlowDuration: {FlowDuration}, FlowIATStd: {FlowIATStd}";
        }
    }
}
