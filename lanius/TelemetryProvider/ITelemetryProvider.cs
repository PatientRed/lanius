namespace lanius
{
    internal interface ITelemetryProvider
    {
        public Dictionary<string, long> Measurements { get; }
        public void Measure();
        public void ContinuousMeasure();
    }
}
