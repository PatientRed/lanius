using lanius.Metrics;
using System.Reflection;

namespace lanius.MetricFactories.Parametrized
{
    public sealed class EmptyMetricConstructionParams : IMetricConstructionParams
    {
        public static Type[] Params => [];

        private EmptyMetricConstructionParams() { }
    }

    public abstract class BaseMetricFactoryParametrized<T, U> : IMetricFactory<T> where T : IMetric
                                                                                  where U : IMetricConstructionParams
    {
        protected static IEnumerable<ConstructorInfo> GetConstructors() => MetricFactoryHelper.GetConstructors<T, U>();

        public abstract T Create(Type type);
    }
}
