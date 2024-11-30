namespace lanius.Metrics
{
    public interface IMeasurable
    {
        void Start();
        void Measure();
        void ContinuousMeasure();
    }
}
