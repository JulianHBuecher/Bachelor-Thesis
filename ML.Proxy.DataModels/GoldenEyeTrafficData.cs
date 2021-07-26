using Microsoft.ML.Data;

namespace ML.Proxy.DataModels
{
    /// <summary>
    /// Data Model for representing GoldenEye Network Traffic
    /// </summary>
    public class GoldenEyeTrafficData
    {
        [LoadColumn(1)]
        public float FlowIATMin;
        [LoadColumn(2)]
        public float FwdIATMin;
        [LoadColumn(3)]
        public float FlowIATMean;
        [LoadColumn(4)]
        public bool Label;

        public GoldenEyeTrafficData() { }

        public GoldenEyeTrafficData(NetworkAttack attack)
        {
            FlowIATMin = attack.FlowIATMin;
            FwdIATMin = attack.FwdIATMin;
            FlowIATMean = attack.FlowIATMean;
        }

        public override string ToString()
        {
            return $"Metrics for GoldenEyeTraffic: FlowIATMin: {FlowIATMin}, FwdIATMin: {FwdIATMin}, FlowIATMean: {FlowIATMean}";
        }
    }
}
