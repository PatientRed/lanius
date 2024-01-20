using lanius.Measurements;

namespace lanius
{
    public abstract class TelemetryProvider : ITelemetryProvider
    {
        private protected IMetric[] _metrics;
        private protected IDataStorageProvider? _storageProvider;

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

        public void Redirect(IDataStorageProvider storageProvider) => _storageProvider = storageProvider;

        public void ForceFlush()
        {
            if (_storageProvider is null)
                throw new InvalidOperationException("You have no storage to flush");

            _storageProvider?.Flush(Measurements);
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
