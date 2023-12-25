using System.Diagnostics;

//ulong instead of uint? how much time process have to be executed to generate overflow of uint?
namespace lanius
{
    public class TelemetryProvider
    {
        private protected IMetric[] _metrics;

        public void GetMeasures()
        {
            foreach (var metric in _metrics)
            {
                metric.Measure();
            }
        }

        internal TelemetryProvider(IMetric[] metrics)
        {
            _metrics = metrics;
        }
    }

    internal abstract class Metric<T> : IMetric
    {
        public abstract long Value { get; }
        public abstract long TotalValue { get; }
        //is it ok? static process field always inside Process.GetCurrentProcess()?
        protected static Process CurrentProcess { get; } = Process.GetCurrentProcess();
        protected T First { get; }
        protected T _previous;
        protected T _last;

        public abstract long Measure();

        public abstract long ContinuosMeasure();

        public Metric(T value) => First = _previous = _last = value;
    }

    internal class TotalCPUTime : Metric<TimeSpan>
    {
        public override long Value => (long)(_last.TotalMilliseconds - _previous.TotalMilliseconds);
        public override long TotalValue => (long)(_last.TotalMilliseconds - First.TotalMilliseconds);

        public override long Measure()
        {
            //alternative?:
            //var threads = CurrentProcess.Threads;
            //TimeSpan threadsTotalTime = new(0);
            //foreach (ProcessThread thread in threads)
            //{
            //    threadsTotalTime += thread.TotalProcessorTime;
            //}
            throw new NotImplementedException();
        }

        public override long ContinuosMeasure()
        {
            throw new NotImplementedException();
        }

        internal TotalCPUTime() : base(CurrentProcess.TotalProcessorTime) { }
    }

    internal class WorkingSetDelta : Metric<long>
    {
        public override long Value => _last - _previous;
        public override long TotalValue => _last - First;

        public override long Measure()
        {
            throw new NotImplementedException();
        }

        public override long ContinuosMeasure()
        {
            throw new NotImplementedException();
        }

        internal WorkingSetDelta() : base(CurrentProcess.WorkingSet64) { }
    }
}
