using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lanius
{
    internal interface ITelemetryProvider
    {
        public Dictionary<string, long> Measurements { get; }
        public void Measure();
        public void ContinuousMeasure();
    }
}
