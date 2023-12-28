using System.Diagnostics;

namespace lanius
{
    internal abstract class Metric<T> : IMetric
    {
        public abstract long Value { get; }
        public abstract long TotalValue { get; }
        protected Process CurrentProcess { get; }
        protected T First { get; }
        protected T _previous;
        protected T _last;

        protected abstract T MeasurementMethod();

        public virtual void Measure()
        {
            _previous = _last;
            _last = MeasurementMethod();
        }

        public virtual void ContinuosMeasure()
        {
            _last = MeasurementMethod();
        }

        public Metric()
        {
            CurrentProcess = Process.GetCurrentProcess();
            First = _previous = _last = MeasurementMethod();
        }
    }
}
