using System.Reflection;

namespace lanius.MetricFactories.Parametrized
{
    public interface IMetricConstructionParams
    {
        static abstract Type[] Params { get; }
    }

    public static class ConstructionParamsExtensions
    {
        public static bool IsCompatible<T>(this ParameterInfo[] actual) where T : IMetricConstructionParams
        {
            if (T.Params.Length != actual.Length)
                return false;

            for (int i = 0; i < T.Params.Length; i++)
                //TODO: equality comparison instead?
                if (actual[i].ParameterType.IsAssignableFrom(T.Params[i]))
                    return false;

            return true;
        }
    }
}
