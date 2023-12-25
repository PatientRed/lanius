namespace lanius
{
    //non-numeric & non-discrete metrics?
    internal interface IMetric
    {
        long Value { get; }
        long TotalValue { get; }

        void Measure();
        void ContinuosMeasure();
    }
}
