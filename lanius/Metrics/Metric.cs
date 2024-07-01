namespace lanius
{
    public abstract class Metric<T> : IMetric
    {
        public abstract string Name { get; }

        public abstract long Value { get; }
        public abstract long TotalValue { get; }

        public DateTime StartTime { get; init; }
        public DateTime EndTime { get; protected set; }

        protected T First { get; init; }
        protected T _previous;
        protected T _last;

        protected abstract T MeasurementMethod();

        public virtual void Measure()
        {
            _previous = _last;

            _last = MeasurementMethod();

            EndTime = DateTime.Now;
        }

        public virtual void ContinuousMeasure()
        {
            _last = MeasurementMethod();

            EndTime = DateTime.Now;
        }

        public Metric()
        {
            StartTime = DateTime.Now;
        }
    }
}
