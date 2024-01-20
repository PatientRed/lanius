using lanius.Measurements;

namespace lanius
{
    public abstract class TelemetryProvider : ITelemetryProvider
    {
        private protected IMetric[] _metrics;

        public IEnumerable<Measurement> Measurements => _metrics.Select(metric => metric.GetData());

        public void Measure()
        {
            foreach (var metric in _metrics)
            {
                metric.Measure();
            }
        }

        public void ContinuousMeasure()
        {
            foreach (var metric in _metrics)
            {
                metric.ContinuosMeasure();
            }
        }

        internal TelemetryProvider(IMetric[] metrics)
        {
            _metrics = metrics;
        }

        public TelemetryProvider() => _metrics = [];
    }

    public class DummyTelemetryProvider : TelemetryProvider
    {
        public DummyTelemetryProvider()
        {
            _metrics = [new TotalCPUTime(), new WorkingSetDelta(), new PagedMemoryDelta(), new PrivateMemoryDelta()];
        }
    }
}
