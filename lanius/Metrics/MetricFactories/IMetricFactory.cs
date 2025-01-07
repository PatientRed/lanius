﻿using lanius.Metrics;

namespace lanius.MetricFactories
{
    public interface IMetricFactory<out T> where T : IMetric
    {
        T Create(Type type);
    }
}
