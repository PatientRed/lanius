namespace lanius.Metrics
{
    //TODO: Regarding struct constraint: I dont wanna convert fields into nullables due to boxing (or either suppress the nullable compiler warning),
    //T value types expected in most usecases, but I could imagine where T is a ref type with 0 boxing overhead, so...
    public abstract class Metric<T> : IMetric where T : struct
    {
        public abstract string Name { get; }

        public abstract long Value { get; }
        public abstract long TotalValue { get; }

        /// <remarks>default(DateTime) here means you forget to call <see cref="Start"/>.</remarks>
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
