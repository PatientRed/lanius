using System.Diagnostics;

namespace lanius
{
    internal abstract class ProcessMetric<T> : Metric<T>
    {
        protected Process CurrentProcess { get; }
        protected virtual bool RefreshRequired => true;

        public override void Measure()
        {
            _previous = _last;

            if (RefreshRequired)
                CurrentProcess.Refresh();

            _last = MeasurementMethod();

            EndTime = DateTime.Now;
        }
        public override void ContinuosMeasure()
        {
            if (RefreshRequired)
                CurrentProcess.Refresh();

            _last = MeasurementMethod();

            EndTime = DateTime.Now;
        }

        public ProcessMetric()
        {
            CurrentProcess = Process.GetCurrentProcess();
        }
    }
}
