using Microsoft.ML.Data;

namespace ML.Proxy.DataModels
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

        public GoldenEyeTrafficData() { }

        public GoldenEyeTrafficData(NetworkAttack attack)
        {
            BwdPktLenStd = attack.BwdPktLenStd;
            FlowIATMin = attack.FlowIATMin;
            FwdIATMin = attack.FwdIATMin;
            FlowIATMean = attack.FlowIATMean;
        }
    }
}
