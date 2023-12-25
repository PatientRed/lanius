namespace lanius
{
    internal interface IMetric
    {
        long Value { get; }
        long TotalValue { get; }

        long Measure();
        long ContinuosMeasure();
    }
}
