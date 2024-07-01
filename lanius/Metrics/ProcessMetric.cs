using System.Diagnostics;

namespace lanius
{
    public abstract class ProcessMetric<T> : Metric<T>
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

        public ProcessMetric()
        {
            CurrentProcess = Process.GetCurrentProcess();
            First = _previous = _last = MeasurementMethod();
        }
    }
}
