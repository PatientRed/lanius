using lanius.Metrics;
using System.Reflection;

namespace lanius.MetricFactories.Parametrized
{
    public abstract class BaseMetricFactoryParametrized<T, U> where T : IMetric
                                                              where U : IMetricConstructionParams
    {
        protected static IEnumerable<ConstructorInfo> GetConstructors() => MetricFactoryHelper.GetConstructors<T, U>();
    }
}
