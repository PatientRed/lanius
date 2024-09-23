using lanius.Metrics;

namespace lanius.MetricFactories
{
    public interface IMetricFactory<T> where T : IMetric
    {
        T Create(Type type);
    }
}
