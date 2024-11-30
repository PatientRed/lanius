namespace lanius.Metrics
{
    //non-numeric & non-discrete metrics?
    public interface IMetric : IMeasurable
    {
        string Name { get; }
        long Value { get; }
        long TotalValue { get; }

        DateTime StartTime { get; }
        DateTime EndTime { get; }
    }

    public interface ICurrentProcessMetric : IMetric { }
    public interface IProcessMetric : IMetric { }
}
