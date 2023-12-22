namespace lanius
{
    internal interface IMetric
    {
        uint Value { get; }
        uint TotalValue { get; }

        void Start();
        uint Measure();
        uint ContinuosMeasure();
    }
}
