//ulong instead of uint? how much time process have to be executed to generate overflow of uint?
namespace lanius
{
    public class TelemetryProvider
    {
        private protected IMetric[] _metrics;

        public void Measure()
        {
            foreach (var metric in _metrics)
            {
                metric.Measure();
            }
        }

        public void ContinuousMeasure()
        {
            foreach (var metric in _metrics)
            {
                metric.ContinuosMeasure();
            }
        }

        internal TelemetryProvider(IMetric[] metrics)
        {
            _metrics = metrics;
        }
    }
}
