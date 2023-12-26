namespace lanius
{
    internal abstract class MemoryDelta : Metric<long>
    {
        public override long Value => _last - _previous;
        public override long TotalValue => _last - First;
    }

    internal class WorkingSetDelta : MemoryDelta
    {
        protected override Func<long> MeasurementMethod() => () => CurrentProcess.WorkingSet64;
    }

    internal class PagedMemoryDelta : MemoryDelta
    {
        protected override Func<long> MeasurementMethod() => () => CurrentProcess.PagedMemorySize64;
    }

    internal class PrivateMemoryDelta : MemoryDelta
    {
        protected override Func<long> MeasurementMethod() => () => CurrentProcess.PrivateMemorySize64;
    }
}
