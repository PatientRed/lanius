namespace lanius.Measurements
{
    public readonly record struct Measurement(string Name, long Value, DateTime StartTime, DateTime EndTime);

    public static class Conversions
    {
        internal static Measurement GetData(this IMetric metric) =>
            new() { Name = metric.Name, Value = metric.Value, StartTime = metric.StartTime, EndTime = metric.EndTime };
    }
}
