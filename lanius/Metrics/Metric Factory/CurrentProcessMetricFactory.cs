using lanius.MetricFactories.Parametrized;
using lanius.Metrics;

namespace lanius.MetricFactories
{
    public class CurrentProcessMetricFactory : BaseMetricFactoryParametrized<ICurrentProcessMetric, EmptyMetricConstructionParams>
    {
        private static readonly Dictionary<Type, Func<ICurrentProcessMetric>> _constructors =
            GetConstructors()
            //.DeclaringType is nullchecked inside MetricFactoryHelper.GetConstructors
            .Select(ci => new KeyValuePair<Type, Func<ICurrentProcessMetric>>(ci.DeclaringType!, () => (ICurrentProcessMetric)ci.Invoke(null)))
            .ToDictionary();

        private static CurrentProcessMetricFactory? _this = null;

        public static CurrentProcessMetricFactory GetFactory() => _this ??= new CurrentProcessMetricFactory();

        public override ICurrentProcessMetric Create(Type type) => _constructors[type].Invoke();

        private CurrentProcessMetricFactory() { }
    }
}
