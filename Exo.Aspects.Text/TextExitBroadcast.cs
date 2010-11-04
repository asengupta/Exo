using System;
using Exo.Aspects.Core;

namespace Exo.Aspects.Text
{
    public class TextExitBroadcast : IExitBroadcast
    {
        public void Run(string description, object enclosingObject)
        {
            Console.Out.WriteLine("{0} - {1}", enclosingObject, description);
        }
    }
}