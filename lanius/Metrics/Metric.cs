using System.Diagnostics;

namespace lanius
{
    internal abstract class Metric<T> : IMetric
    {
        public abstract string Name { get; }

        public abstract long Value { get; }
        public abstract long TotalValue { get; }

        public DateTime StartTime { get; init; }
        public DateTime EndTime { get; private set; }

        //Do I need to extract this (and measurement methods) as part of something like ProcessMetric<T>:Metric<T> ?
        //f.e. I want implement system-wide metric not specific to current process and measure them in parallel, in this case current abstraction is not abstract enough.
        protected Process CurrentProcess { get; }
        protected virtual bool RefreshRequired => true;

        protected T First { get; }
        protected T _previous;
        protected T _last;

        protected abstract T MeasurementMethod();

        public virtual void Measure()
        {
            _previous = _last;

            if (RefreshRequired)
                CurrentProcess.Refresh();

            _last = MeasurementMethod();

            EndTime = DateTime.Now;
        }

        public virtual void ContinuosMeasure()
        {
            if (RefreshRequired)
                CurrentProcess.Refresh();

            _last = MeasurementMethod();

            EndTime = DateTime.Now;
        }

        public Metric()
        {
            StartTime = DateTime.Now;
            CurrentProcess = Process.GetCurrentProcess();
            First = _previous = _last = MeasurementMethod();
        }
    }
}
