using System;
using Exo.Aspects.Core;

namespace Exo.Aspects.Text
{
    public class TextPinger : AbstractPinger
    {
        public TextPinger(string methodDescription) : base(methodDescription, Guid.NewGuid())
        {
        }

        public override void Start()
        {
            Console.Out.WriteLine("{0} started...{1}@{2}", methodDescription, guid, DateTime.Now);
        }

        public override void End()
        {
            Console.Out.WriteLine("{0} stopped...{1}@{2}", methodDescription, guid, DateTime.Now);
        }
    }
}