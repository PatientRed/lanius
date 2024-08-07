﻿namespace lanius
{
    //non-numeric & non-discrete metrics?
    public interface IMetric
    {
        string Name { get; }
        long Value { get; }
        long TotalValue { get; }

        DateTime StartTime { get; }
        DateTime EndTime { get; }

        void Measure();
        void ContinuousMeasure();
    }
}
