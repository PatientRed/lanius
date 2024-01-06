namespace lanius
{
    internal abstract class MemoryDelta : Metric<long>
    {
        public override long Value => _last - _previous;
        public override long TotalValue => _last - First;
    }

    internal class WorkingSetDelta : MemoryDelta
    {
        public override string Name => "WorkingSetDelta";
        protected override long MeasurementMethod() => CurrentProcess.WorkingSet64;
    }

    internal class PagedMemoryDelta : MemoryDelta
    {
        public override string Name => "PagedMemoryDelta";

        protected override long MeasurementMethod() => CurrentProcess.PagedMemorySize64;
    }

    internal class PrivateMemoryDelta : MemoryDelta
    {
        public override string Name => "PrivateMemoryDelta";
        protected override long MeasurementMethod() => CurrentProcess.PrivateMemorySize64;
    }
}
