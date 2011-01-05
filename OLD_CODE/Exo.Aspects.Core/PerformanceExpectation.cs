using System;

namespace Exo.Aspects.Core
{
    public class PerformanceExpectation
    {
        public PerformanceExpectation(int maximum)
        {
            Maximum = maximum;
        }

        public int Maximum { get; private set; }

        public bool IsMetBy(TimeSpan duration)
        {
            return duration.TotalSeconds <= Maximum;
        }
    }
}