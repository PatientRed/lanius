using lanius.MetricFactories.ConstructionParameters;
using lanius.Metrics;

namespace lanius.MetricFactories
{
    public class ProcessMetricParams : IMetricConstructionParams
    {
        private static Type[] _params = [typeof(int)];
        private static ProcessMetricParams? _this;

        public static Type[] Params => _params;

        private ProcessMetricParams() { }
    }

    public class ProcessMetricFactoryOriginal : IMetricFactory<IProcessMetric>
    {
        private static readonly Dictionary<Type, Func<int, IProcessMetric>> _constructors = MetricFactoryHelper.GetConstructors<IProcessMetric>()
                                                                                .Where(ci => ci.GetParameters().Length == 1 && ci.GetParameters()[0].ParameterType == typeof(int))
                                                                                //.DeclaringType is nullchecked inside MetricFactoryHelper.GetConstructors
                                                                                .Select(ci => new KeyValuePair<Type, Func<int, IProcessMetric>>(ci.DeclaringType!, (input) => (IProcessMetric)ci.Invoke([input])))
                                                                                .ToDictionary();

        public static ProcessMetricFactoryOriginal GetFactory(int processID) => new ProcessMetricFactoryOriginal(processID);

        public IProcessMetric Create(Type type) => _constructors[type].Invoke(_processId);

        private readonly int _processId;

        internal ProcessMetricFactoryOriginal(int id) => _processId = id;
    }
}
