using lanius.MetricFactories.Parametrized;
using lanius.Metrics;

namespace lanius.MetricFactories
{
    public class SimpleMetricFactory : BaseMetricFactoryParametrized<IMetric, EmptyMetricConstructionParams>
    {
        private static readonly Dictionary<Type, Func<IMetric>> _constructors =
            GetConstructors()
            //.DeclaringType is nullchecked inside MetricFactoryHelper.GetConstructors
            .Select(ci => new KeyValuePair<Type, Func<IMetric>>(ci.DeclaringType!, () => (IMetric)ci.Invoke(null)))
            .ToDictionary();

        private static SimpleMetricFactory? _this = null;

        public static SimpleMetricFactory GetFactory() => _this ??= new SimpleMetricFactory();

        public override IMetric Create(Type type) => _constructors[type].Invoke();

        private SimpleMetricFactory() { }
    }
}
