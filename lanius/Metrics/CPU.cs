namespace lanius
{
    internal abstract class CPUTime : Metric<TimeSpan>
    {
        protected override bool RefreshRequired => false;
        public TimeSpan RawValue => _last - _previous;
        public TimeSpan RawTotalValue => _last - First;
        public override long Value => (long)(_last - _previous).TotalMilliseconds; //Raw.total to long?
        public override long TotalValue => (long)(_last - First).TotalMilliseconds; //Raw.total to long?
    }

    internal class TotalCPUTime : CPUTime
    {
        public override string Name => "TotalCPUTime";
        //alternative?:
        //var threads = CurrentProcess.Threads;
        //TimeSpan threadsTotalTime = new(0);
        //foreach (ProcessThread thread in threads)
        //{
        //    threadsTotalTime += thread.TotalProcessorTime;
        //}
        protected override TimeSpan MeasurementMethod() => CurrentProcess.TotalProcessorTime;
    }
}
