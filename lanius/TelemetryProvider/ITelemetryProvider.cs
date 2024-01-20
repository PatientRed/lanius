using lanius.Measurements;

namespace lanius
{
    internal interface ITelemetryProvider
    {
        public IEnumerable<Measurement> Measurements { get; }
        public void Measure();
        public void ContinuousMeasure();
    }
}
