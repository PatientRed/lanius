using lanius.DataStorage;
using lanius.Measurements;
using lanius.MetricFactories;
using lanius.Metrics;

namespace lanius.TelemetryProviders
{
    public interface ITelemetryProvider<T, U> where T : ITelemetryProvider<T, U>
                                              where U : IMetric
    {
        public static abstract T CreateProvider(IEnumerable<Type> metrics, IMetricFactory<U> factory, IDataStorageProvider? storageProvider = null);

        public IEnumerable<Measurement> Measurements { get; }
        public void Measure();
        public void ContinuousMeasure();
    }
}
