namespace lanius.Measurements
{
    public readonly record struct Measurement(string Name, long Value, DateTime StartTime, DateTime EndTime);
}
