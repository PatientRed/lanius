namespace lanius.MetricFactories.Parametrized
{
    public sealed class EmptyMetricConstructionParams : IMetricConstructionParams
    {
        public static Type[] Params => [];

        private EmptyMetricConstructionParams() { }
    }
}
