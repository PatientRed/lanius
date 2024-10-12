using lanius.MetricFactories.Parametrized;
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

    public class ProcessMetricFactoryAlt2 : BaseMetricFactoryParametrized<IProcessMetric, ProcessMetricParams>,
                                            IMetricFactory<IProcessMetric>
    {
        private static readonly Dictionary<Type, Func<int, IProcessMetric>> _constructors =
            GetConstructors()
            //.DeclaringType is nullchecked inside MetricFactoryHelper.GetConstructors
            .Select(ci => new KeyValuePair<Type, Func<int, IProcessMetric>>(ci.DeclaringType!, (input) => (IProcessMetric)ci.Invoke([input])))
            .ToDictionary();

        public static ProcessMetricFactoryAlt2 GetFactory(int processID) => new ProcessMetricFactoryAlt2(processID);

        public IProcessMetric Create(Type type) => _constructors[type].Invoke(_processId);

        private readonly int _processId;

        internal ProcessMetricFactoryAlt2(int id) => _processId = id;
    }
}
