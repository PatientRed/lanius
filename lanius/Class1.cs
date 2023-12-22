using System.Diagnostics;

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
        public virtual uint Value { get; }
        public virtual uint TotalValue { get; }
        //is it ok? static process field always inside Process.GetCurrentProcess()?
        protected static Process CurrentProcess { get; } = Process.GetCurrentProcess();
        protected T First { get; }
        protected T _previous;
        protected T _last;

        public virtual uint Measure()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            var _process = Process.GetCurrentProcess();
            var processTime = _process.TotalProcessorTime;
            var memory = _process.WorkingSet64;

        }

        public virtual uint ContinuosMeasure()
        {
            throw new NotImplementedException();
        }

        public Metric(T value) => First = _previous = _last = value;
    }

    internal class TotalCPUTime : Metric<TimeSpan>
    {
        public override uint Value => (uint)(_last.TotalMilliseconds - _previous.TotalMilliseconds);
        public override uint TotalValue => (uint)(_last.TotalMilliseconds - First.TotalMilliseconds);

        internal TotalCPUTime() : base(CurrentProcess.TotalProcessorTime) { }
    }

    internal class WorkingSetDelta : Metric<long>
    {
        internal WorkingSetDelta() : base(CurrentProcess.WorkingSet64) { }
    }
}
