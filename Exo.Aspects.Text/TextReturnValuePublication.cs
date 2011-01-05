using System;
using Exo.Aspects.Core;

namespace Exo.Aspects.Text
{
    public class TextReturnValuePublication : IReturnValuePublication
    {
        public void Run(object returnValue, string description)
        {
//            Console.Out.WriteLine("{0}/{1}...", enclosingObject.GetType(), returnValue);
            Console.Out.WriteLine("{0}/{1}...", returnValue, returnValue);

        }
    }
}