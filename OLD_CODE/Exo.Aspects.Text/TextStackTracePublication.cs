using System;
using System.Diagnostics;
using System.Text;
using Exo.Aspects.Core;

namespace Exo.Aspects.Text
{
    public class TextStackTracePublication : IStackTracePublication
    {
        public void Run(StackTrace trace)
        {
            var builder = new StringBuilder();
            Console.Out.WriteLine(trace);
        }
    }
}