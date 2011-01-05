using System;
using System.Collections.Generic;
using Exo.Aspects.Core;

namespace Exo.Aspects.Text
{
    public class TextArgumentPublication : IArgumentPublication
    {
        public void Run(List<object> arguments, string description)
        {
            Console.Out.WriteLine("Argument Publish Hook " + arguments.Count);
            foreach (var o in arguments)
            {
                Console.Out.WriteLine("o = {0}", o);
            }
            var varargs = (object[])arguments[arguments.Count - 1];
            foreach (var o in varargs)
            {
                Console.Out.WriteLine("vararg = {0}", o);
            }
        }
    }
}