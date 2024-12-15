using lanius.MetricFactories.Parametrized;
using lanius.Metrics;

namespace lanius.MetricFactories
{
    public class ProcessMetricParams : IMetricConstructionParams
    {
        private static readonly Type[] _params = [typeof(int)];

        public static Type[] Params => _params;

        private ProcessMetricParams() { }
    }

    public class ProcessMetricFactory : BaseMetricFactoryParametrized<IProcessMetric, ProcessMetricParams>
    {
        private static readonly Dictionary<Type, Func<int, IProcessMetric>> _constructors =
            GetConstructors()
            //.DeclaringType is nullchecked inside MetricFactoryHelper.GetConstructors
            .Select(ci => new KeyValuePair<Type, Func<int, IProcessMetric>>(ci.DeclaringType!, (input) => (IProcessMetric)ci.Invoke([input])))
            .ToDictionary();

        public static ProcessMetricFactory GetFactory(int processID) => new ProcessMetricFactory(processID);

        public override IProcessMetric Create(Type type) => _constructors[type].Invoke(_processId);

        private readonly int _processId;

        private ProcessMetricFactory(int id) => _processId = id;
    }
}
