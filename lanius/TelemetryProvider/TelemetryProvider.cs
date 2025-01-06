using lanius.DataStorage;
using lanius.Measurements;
using lanius.MetricFactories;
using lanius.Metrics;

//since this is suggested as entry-point, should be the pure namespace? (same logic applied to base interface)
namespace lanius.TelemetryProviders
{
    public sealed class TelemetryProvider<T> : IMeasurable, ITelemetryProvider<TelemetryProvider<T>, T> where T : IMetric
    {
        private readonly IEnumerable<T> _metrics;
        private IDataStorageProvider? _storageProvider;

        private bool _cached = false;
        private IEnumerable<Measurement>? _cachedMeasurements;
        public IEnumerable<Measurement> Measurements
        {
            get
            {
                if (!_cached)
                {
                    _cachedMeasurements = _metrics.Select(metric => metric.GetData()).ToArray();
                    _cached = true;
                }

                return _cachedMeasurements ?? throw new InvalidOperationException("Caching mechanism is broken");
                //I can only imagine programming issue in future lead to this behaviour or reflection-based intrusion. Safe workaround seems to be:
                //return _cachedMeasurements ??= _metrics.Select(metric => metric.GetData());
            }
        }

        public void Start()
        {
            foreach (var metric in _metrics)
            {
                metric.Start();
            }
        }

        public void Measure()
        {
            foreach (var metric in _metrics)
            {
                metric.Measure();
            }

            _cached = false;
        }

        public void ContinuousMeasure()
        {
            foreach (var metric in _metrics)
            {
                metric.ContinuousMeasure();
            }

            _cached = false;
        }

        public void Redirect(IDataStorageProvider storageProvider) => _storageProvider = storageProvider;

        public void ForceFlush()
        {
            if (_storageProvider is null)
                throw new InvalidOperationException("You have no storage to flush");

            _storageProvider.Flush(Measurements);
        }

        //TODO: now highly-coupled with factory? cannot support metrics from different subclasses (without common ancestor)
        public static TelemetryProvider<T> CreateProvider(IEnumerable<Type> metrics, IMetricFactory<T> factory, IDataStorageProvider? storageProvider = null)
                                            => new TelemetryProvider<T>(metrics.Where(metric => typeof(T).IsAssignableFrom(metric)).Select(factory.Create), storageProvider);

        internal TelemetryProvider(IEnumerable<T> metrics, IDataStorageProvider? storageProvider = null)
        {
            _metrics = metrics.ToArray();
            Redirect(storageProvider!);
        }

        //Only useful when using custom metrics adding behaviour. And since the field is now readonly it is not possible.
        internal TelemetryProvider() : this(metrics: [], storageProvider: null!) { }
    }
}
