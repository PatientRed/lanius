using lanius.DataStorage;
using lanius.MetricFactories;
using lanius.Metrics;

namespace lanius.TelemetryProviders
{
    public class SimpleTelemetryProvider : TelemetryProvider<IMetric>
    {
        public static new SimpleTelemetryProvider CreateProvider(IEnumerable<Type> metrics, IMetricFactory<IMetric> factory, IDataStorageProvider? storageProvider = null)
                                                  => new SimpleTelemetryProvider(metrics.Where(factory.CanCreate).Select(factory.Create), storageProvider);

        internal SimpleTelemetryProvider(IEnumerable<IMetric> metrics, IDataStorageProvider? storageProvider = null) : base(metrics, storageProvider) { }
    }
}
