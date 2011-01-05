using System;
using Exo.Aspects.Core;

namespace Exo.Aspects.Text
{
    public class TextSelfPublication : ISelfPublication
    {
        public void Run(object self, string description)
        {
            Console.Out.WriteLine("{0} published self...", self);
        }
    }
}