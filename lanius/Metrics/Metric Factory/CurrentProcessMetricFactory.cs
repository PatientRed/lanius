using lanius.Metrics;

namespace lanius.MetricFactories
{
    public class CurrentProcessMetricFactory : IMetricFactory<ICurrentProcessMetric>
    {
        private static Dictionary<Type, Func<ICurrentProcessMetric>> _constructors =
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                                                   .Where(type => typeof(ICurrentProcessMetric).IsAssignableFrom(type) && type.GetConstructors().Where(constructor => constructor.GetParameters().Length == 0).Any())
                                                   .Select(type => new KeyValuePair<Type, Func<ICurrentProcessMetric>>(type, () => (ICurrentProcessMetric)type.GetConstructors().First(constructor => constructor.GetParameters().Length == 0).Invoke(null)))
                                                   .ToDictionary();

        private static CurrentProcessMetricFactory? _this = null;

        public static CurrentProcessMetricFactory GetFactory() => _this ??= new CurrentProcessMetricFactory();

        public ICurrentProcessMetric Create(Type type) => _constructors[type].Invoke();

        private CurrentProcessMetricFactory() { }
    }
}
