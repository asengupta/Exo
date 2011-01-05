using System;
using Exo.Aspects.Core;

namespace Exo.Aspects.Text
{
    public class TextReturnValuePublication : IReturnValuePublication
    {
        public void Run(object enclosingObject, object returnValue)
        {
            Console.Out.WriteLine("{0}/{1}...", enclosingObject.GetType(), returnValue);
        }
    }
}