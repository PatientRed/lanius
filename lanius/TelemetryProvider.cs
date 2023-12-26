//ulong instead of uint? how much time process have to be executed to generate overflow of uint?

namespace lanius
{
    public abstract class TelemetryProvider : ITelemetryProvider
    {
        private protected IMetric[] _metrics;

        public Dictionary<string, long> Measurements => _metrics.ToDictionary(metric => metric.GetType().ToString(), metric => metric.Value);

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

        public TelemetryProvider() => _metrics = [];
    }
}
