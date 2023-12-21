using System.Diagnostics;

namespace lanius
{
    public class TelemetryProvider
    {

    }

    internal class Metric
    {
        internal int Value { get; private set; }
        int _first;
        int _last;

        private int Measure()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            var _process = Process.GetCurrentProcess();
            var processTime = _process.TotalProcessorTime;
            var memory = _process.WorkingSet64;
        }

        public void End()
        {

        }

        public Metric()
        {
        }
    }
}
