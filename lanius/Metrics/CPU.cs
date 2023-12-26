namespace lanius
{
    internal abstract class CPUTime : Metric<TimeSpan>
    {
        public TimeSpan RawValue => _last - _previous;
        public TimeSpan RawTotalValue => _last - First;
        public override long Value => (long)(_last.TotalMilliseconds - _previous.TotalMilliseconds);
        public override long TotalValue => (long)(_last.TotalMilliseconds - First.TotalMilliseconds);
    }

    internal class TotalCPUTime : CPUTime
    {
        //alternative?:
        //var threads = CurrentProcess.Threads;
        //TimeSpan threadsTotalTime = new(0);
        //foreach (ProcessThread thread in threads)
        //{
        //    threadsTotalTime += thread.TotalProcessorTime;
        //}
        protected override Func<TimeSpan> MeasurementMethod() => () => CurrentProcess.TotalProcessorTime;
    }
}
