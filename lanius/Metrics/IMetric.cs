namespace lanius
{
    //non-numeric & non-discrete metrics?
    internal interface IMetric
    {
        string Name { get; }
        long Value { get; }
        long TotalValue { get; }

        void Measure();
        void ContinuosMeasure();
    }
}
