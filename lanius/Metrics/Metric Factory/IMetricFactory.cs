namespace lanius
{
    internal interface IMetricFactory<T> where T : IMetric
    {
        T Create(Type type);
    }
}
