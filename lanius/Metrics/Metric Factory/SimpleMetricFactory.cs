namespace lanius
{
    public class SimpleMetricFactory : IMetricFactory<IMetric>
    {
        private static Dictionary<Type, Func<IMetric>> _constructors =
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                                                   .Where(type => typeof(IMetric).IsAssignableFrom(type) && type.GetConstructors().Where(constructor => constructor.GetParameters().Length == 0).Any())
                                                   .Select(type => new KeyValuePair<Type, Func<IMetric>>(type, () => (IMetric)type.GetConstructors().First(constructor => constructor.GetParameters().Length == 0).Invoke(null)))
                                                   .ToDictionary();

        private static SimpleMetricFactory? _this = null;

        public static SimpleMetricFactory GetFactory() => _this ??= new SimpleMetricFactory();

        public IMetric Create(Type type) => _constructors[type].Invoke();
    }
}
