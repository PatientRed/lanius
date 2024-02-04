namespace lanius
{
    public abstract class MemoryDelta : ProcessMetric<long>
    {
        public override long Value => _last - _previous;
        public override long TotalValue => _last - First;
    }

    public class WorkingSetDelta : MemoryDelta
    {
        public override string Name => "WorkingSetDelta";
        protected override long MeasurementMethod() => CurrentProcess.WorkingSet64;
    }

    public class PagedMemoryDelta : MemoryDelta
    {
        public override string Name => "PagedMemoryDelta";

        protected override long MeasurementMethod() => CurrentProcess.PagedMemorySize64;
    }

    public class PrivateMemoryDelta : MemoryDelta
    {
        public override string Name => "PrivateMemoryDelta";
        protected override long MeasurementMethod() => CurrentProcess.PrivateMemorySize64;
    }
}
