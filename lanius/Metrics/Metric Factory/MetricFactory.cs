namespace lanius
{
    internal class MetricFactory : IMetricFactory<IMetric>
    {
        private static Dictionary<Type, Func<IMetric>> _constructors =
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                                                   .Where(type => typeof(IMetric).IsAssignableFrom(type) && type.GetConstructors().Where(constructor => constructor.GetParameters().Length == 0).Any())
                                                   .Select(type => new KeyValuePair<Type, Func<IMetric>>(type, () => (IMetric)type.GetConstructors().First(constructor => constructor.GetParameters().Length == 0).Invoke(null)))
                                                   .ToDictionary();

        private static MetricFactory? _this = null;

        public static MetricFactory GetFactory() => _this ??= new MetricFactory();

        public IMetric Create(Type type) => _constructors[type].Invoke();
    }
}
