using System.Diagnostics;

namespace lanius
{
    internal abstract class Metric<T> : IMetric
    {
        public abstract long Value { get; }
        public abstract long TotalValue { get; }
        //is it ok? static process field always inside Process.GetCurrentProcess()?
        protected static Process CurrentProcess { get; } = Process.GetCurrentProcess();
        protected T First { get; }
        protected T _previous;
        protected T _last;

        protected abstract Func<T> MeasurementMethod();

        public virtual void Measure()
        {
            _previous = _last;
            _last = MeasurementMethod().Invoke();
        }

        public virtual void ContinuosMeasure()
        {
            _last = MeasurementMethod().Invoke();
        }

        public Metric() => First = _previous = _last = MeasurementMethod().Invoke();
    }

    internal class TotalCPUTime : Metric<TimeSpan>
    {
        public TimeSpan RawValue => _last - _previous;
        public TimeSpan RawTotalValue => _last - First;
        public override long Value => (long)(_last.TotalMilliseconds - _previous.TotalMilliseconds);
        public override long TotalValue => (long)(_last.TotalMilliseconds - First.TotalMilliseconds);

        //alternative?:
        //var threads = CurrentProcess.Threads;
        //TimeSpan threadsTotalTime = new(0);
        //foreach (ProcessThread thread in threads)
        //{
        //    threadsTotalTime += thread.TotalProcessorTime;
        //}
        protected override Func<TimeSpan> MeasurementMethod() => () => CurrentProcess.TotalProcessorTime;
    }

    internal class WorkingSetDelta : Metric<long>
    {
        public override long Value => _last - _previous;
        public override long TotalValue => _last - First;

        protected override Func<long> MeasurementMethod() => () => CurrentProcess.WorkingSet64;
    }
}
