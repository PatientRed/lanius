namespace lanius
{
    internal interface IMetric
    {
        long Value { get; }
        long TotalValue { get; }

        void Measure();
        void ContinuosMeasure();
    }
}
