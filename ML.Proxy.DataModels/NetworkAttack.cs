namespace ML.Proxy.DataModels
{
    /// <summary>
    /// Klasse, die die Eigenschaften für alle Angriffsszenarien zusammenfasst
    /// </summary>
    public class NetworkAttack
    {
        // GoldenEye
        //public float BwdPktLenStd { get; set; }
        public float FlowIATMin { get; set; }
        public float FwdIATMin { get; set; }
        public float FlowIATMean { get; set; }

        // LOIC (ohne BwdPktLenStd)
        public float PktSizeAvg { get; set; }
        public float FlowDuration { get; set; }
        public float FlowIATStd { get; set; }

        // Slowloris (ohne FlowDuration und FwdIATMin)
        //public float BwdIATMean { get; set; }
        public float FwdIATMean { get; set; }
    }
}
