using System.Diagnostics;

namespace lanius.Metrics.CurrentProcess
{
    public abstract class CurrentProcessMetric<T> : Metric<T>, ICurrentProcessMetric where T : struct
    {
        protected Process CurrentProcess { get; init; }
        protected virtual bool RefreshRequired => true;

        public override void Measure()
        {
            if (RefreshRequired)
                CurrentProcess.Refresh();

            base.Measure();
        }
        public override void ContinuousMeasure()
        {
            if (RefreshRequired)
                CurrentProcess.Refresh();

            base.ContinuousMeasure();
        }

        public CurrentProcessMetric()
        {
            CurrentProcess = Process.GetCurrentProcess();
        }
    }
}
