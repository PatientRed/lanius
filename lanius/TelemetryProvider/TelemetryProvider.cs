using lanius.Measurements;

namespace lanius
{
    public class TelemetryProvider : ITelemetryProvider
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

            _storageProvider.Flush(Measurements);
        }

        public static TelemetryProvider CreateProvider(IEnumerable<Type> metrics) => new TelemetryProvider(metrics.Where(metric => typeof(IMetric).IsAssignableFrom(metric)).Select(MetricFactory.Create).ToArray());

        internal TelemetryProvider(IMetric[] metrics, IDataStorageProvider? storageProvider = null)
        {
            _metrics = metrics;
            Redirect(storageProvider!);
        }

        //Only useful when using custom metrics adding behaviour
        public TelemetryProvider() : this(metrics: [], storageProvider: null!) { }
    }
}
