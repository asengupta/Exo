using System;
using Exo.Aspects.Core;

namespace Exo.Aspects.Text
{
    public class TextExecutionTimeMonitor : IExecutionTimeMonitor
    {
        private readonly DateTime start;
        private readonly PerformanceExpectation expectation;

        public TextExecutionTimeMonitor(DateTime start, PerformanceExpectation expectation)
        {
            this.start = start;
            this.expectation = expectation;
            Console.Out.WriteLine("Monitoring for performance (maximum is {0})", expectation.Maximum);
        }

        public void End()
        {
            var end = DateTime.Now;
            var duration = end - start;
            if (expectation.IsMetBy(duration))
                Console.Out.WriteLine("Expected performance met!");
            else Console.Out.WriteLine("Method took longer by {0} seconds.", duration.TotalSeconds - expectation.Maximum);
        }
    }
}