using lanius.Metrics;

namespace lanius.MetricFactories
{
    public interface IMetricFactory<out T> where T : IMetric
    {
        T Create(Type type);

        //TODO: seems not good. Think more.
        bool CanCreate(Type type) => typeof(T).IsAssignableFrom(type);
    }
}
