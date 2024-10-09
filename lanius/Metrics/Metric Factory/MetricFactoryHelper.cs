using lanius.Metrics;
using System.Reflection;

namespace lanius.MetricFactories
{
    public static class MetricFactoryHelper
    {
        public static IEnumerable<ConstructorInfo> GetConstructors<T>() where T : IMetric =>
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                                                   .Where(type => typeof(T).IsAssignableFrom(type))
                                                   .SelectMany(type => type.GetConstructors().Where(ci => ci.DeclaringType is not null && !ci.DeclaringType.IsAbstract && !ci.IsStatic && ci.IsPublic));
    }
}
