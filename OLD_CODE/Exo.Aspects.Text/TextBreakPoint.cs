using System;
using Exo.Aspects.Core;

namespace Exo.Aspects.Text
{
    public class TextBreakPoint : IBreakPoint
    {
        public void Activate(int startLine, int startColumn, int endLine, int endColumn, string documentURL)
        {
            Console.Out.WriteLine(string.Format("{0}[{1}]>>{2}[{3}] in {4}", startLine, startColumn, endLine, endColumn, documentURL));
        }
//        public void Activate(string documentURL)
//        {
//            Console.Out.WriteLine(documentURL);
//        }
    }
}