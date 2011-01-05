using System;

namespace Exo.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class Broadcast : Attribute
    {
        public string Description
        {
            get; set;
        }
    }
}