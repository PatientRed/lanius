namespace lanius.Metrics
{
    //non-numeric & non-discrete metrics?
    public interface IMetric
    {
        string Name { get; }
        long Value { get; }
        long TotalValue { get; }

        DateTime StartTime { get; }
        DateTime EndTime { get; }

        void Start();
        void Measure();
        void ContinuousMeasure();
    }

    public interface ICurrentProcessMetric : IMetric { }
    public interface IProcessMetric : IMetric { }
}
