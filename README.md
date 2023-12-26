# lanius
Performance telemetry

usage:

	var telemetry = new DummyTelemetryProvider();
    
    //some code here
        
    telemetry.Measure();

    foreach (var metric in telemetry.Measurements)
      Console.WriteLine($"{metric.Key}={metric.Value}");