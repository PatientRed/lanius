using lanius.Measurements;
using lanius.MetricFactories;
using lanius.Metrics;

namespace lanius.TelemetryProviders
{
    public sealed class TelemetryProvider<U> : ITelemetryProvider<TelemetryProvider<U>, U> where U : IMetric
    {
        private U[] _metrics;
        private IDataStorageProvider? _storageProvider;

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
                metric.ContinuousMeasure();
            }
        }

        public void Redirect(IDataStorageProvider storageProvider) => _storageProvider = storageProvider;

        public void ForceFlush()
        {
            if (_storageProvider is null)
                throw new InvalidOperationException("You have no storage to flush");

            _storageProvider.Flush(Measurements);
        }

        //TODO: now highly-coupled with factory? cannot support metrics from different subclasses
        public static TelemetryProvider<U> CreateProvider(IEnumerable<Type> metrics, IMetricFactory<U> factory, IDataStorageProvider? storageProvider = null)
                                            => new TelemetryProvider<U>(metrics.Where(metric => typeof(U).IsAssignableFrom(metric)).Select(factory.Create).ToArray(), storageProvider);

        internal TelemetryProvider(U[] metrics, IDataStorageProvider? storageProvider = null)
        {
            _metrics = metrics;
            Redirect(storageProvider!);
        }

        //Only useful when using custom metrics adding behaviour
        internal TelemetryProvider() : this(metrics: [], storageProvider: null!) { }
    }
}
