using lanius.Metrics;

namespace lanius.MetricFactories
{
    public class ProcessMetricFactory : IMetricFactory<IProcessMetric>
    {
        private static Dictionary<Type, Func<int, IProcessMetric>> _constructors =
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                                                   .Where(type => typeof(IProcessMetric).IsAssignableFrom(type) && type.GetConstructors().Where(constructor => constructor.GetParameters().Length == 1).Any())
                                                   .Select(type => new KeyValuePair<Type, Func<int, IProcessMetric>>(type, (input) => (IProcessMetric)type.GetConstructors().First(constructor => constructor.GetParameters().Length == 1).Invoke([input])))
                                                   .ToDictionary();

        public static ProcessMetricFactory GetFactory(int processID) => new ProcessMetricFactory(processID);

        public IProcessMetric Create(Type type) => _constructors[type].Invoke(_processId);

        private readonly int _processId;

        internal ProcessMetricFactory(int id) => _processId = id;
    }
}
