namespace lanius
{
    internal interface IMetric
    {
        long Value { get; }
        long TotalValue { get; }

        void Start();
        long Measure();
        long ContinuosMeasure();
    }
}
