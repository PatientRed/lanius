namespace lanius.Metrics
{
    public abstract class Metric<T> : IMetric
    {
        public abstract string Name { get; }

        public abstract long Value { get; }
        public abstract long TotalValue { get; }

        public DateTime StartTime { get; protected set; }
        public DateTime EndTime { get; protected set; }

        protected T _first;
        protected T _previous;
        protected T _last;

        protected abstract T MeasurementMethod();

        public virtual void Start()
        {
            _first = _previous = _last = MeasurementMethod();
            StartTime = DateTime.Now;
        }

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
    }
}
