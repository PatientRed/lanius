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
        public virtual long Value { get; }
        public virtual long TotalValue { get; }
        //is it ok? static process field always inside Process.GetCurrentProcess()?
        protected static Process CurrentProcess { get; } = Process.GetCurrentProcess();
        protected T First { get; }
        protected T _previous;
        protected T _last;

        public virtual long Measure()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            var _process = Process.GetCurrentProcess();
            var memory = _process.WorkingSet64;

            var threads = _process.Threads;
            TimeSpan threadsTotalTime = new(0);
            foreach (ProcessThread thread in threads)
            {
                threadsTotalTime += thread.TotalProcessorTime;
            }
        }

        public virtual long ContinuosMeasure()
        {
            throw new NotImplementedException();
        }

        public Metric(T value) => First = _previous = _last = value;
    }

    internal class TotalCPUTime : Metric<TimeSpan>
    {
        public override long Value => (long)(_last.TotalMilliseconds - _previous.TotalMilliseconds);
        public override long TotalValue => (long)(_last.TotalMilliseconds - First.TotalMilliseconds);

        internal TotalCPUTime() : base(CurrentProcess.TotalProcessorTime) { }
    }

    internal class WorkingSetDelta : Metric<long>
    {
        internal WorkingSetDelta() : base(CurrentProcess.WorkingSet64) { }
    }
}
