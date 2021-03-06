using Microsoft.ML.Data;

namespace ML.Proxy.DataModels
{
    /// <summary>
    /// Data Model for Representing Slowloris Network Traffic
    /// </summary>
    public class SlowlorisTrafficData
    {
        [LoadColumn(0)]
        public float FlowDuration;
        [LoadColumn(2)]
        public float FwdIATMin;
        [LoadColumn(3)]
        public float FwdIATMean;
        [LoadColumn(4)]
        public bool Label;

        public SlowlorisTrafficData() { }

        public SlowlorisTrafficData(NetworkAttack attack)
        {
            FlowDuration = attack.FlowDuration;
            FwdIATMin = attack.FwdIATMin;
            FwdIATMean = attack.FwdIATMean;
        }

        public override string ToString()
        {
            return $"Metrics for SlowlorisTraffic: FlowDuration: {FlowDuration}, FwdIATMin: {FwdIATMin}, FwdIATMean: {FwdIATMean}";
        }
    }
}
