using System.Diagnostics;

namespace lanius
{
    public class TelemetryProvider
    {

    }

    internal abstract class Metric<T> : IMetric
    {
        public virtual uint Value { get; }
        public virtual uint TotalValue { get; }
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
}
