# lanius
Performance telemetry

usage:

As is:

	//set of commonly used for your needs
	Type[] dummyMetrics = [typeof(TotalCPUTime), typeof(WorkingSetDelta), typeof(PagedMemoryDelta), typeof(PrivateMemoryDelta)];
	var telemetry = TelemetryProvider.CreateProvider(dummyMetrics, new TextFileProvider("test.txt"));
	//or with custom metric selection
	//var telemetry = TelemetryProvider.CreateProvider([typeof(TotalCPUTime), typeof(WorkingSetDelta)], new TextFileProvider("test.txt"));

	//......
	//some code here
	//......

	telemetry.Measure();
	telemetry.ForceFlush();

	//or look inside test.txt
	foreach (var metric in telemetry.Measurements)
		Console.WriteLine($"{metric.Key}={metric.Value}");

With added custom metric:

	public class NonPagedMemoryDelta : MemoryDelta
	{
		public override string Name => "NonPagedMemoryDelta";

		protected override long MeasurementMethod() => CurrentProcess.NonpagedSystemMemorySize64;
	}
	
	TelemetryProvider telemetryCreation() => TelemetryProvider.CreateProvider(dummyMetrics.Append(typeof(NonPagedMemoryDelta)), new TextFileProvider("test.txt"));