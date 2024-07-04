namespace lanius
{
    public interface IMetricFactory<T> where T : IMetric
    {
        T Create(Type type);
    }
}
