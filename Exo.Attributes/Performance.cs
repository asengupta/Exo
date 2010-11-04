using System;

namespace Exo.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class Performance : Attribute
    {
        public int Maximum { get; set; }
    }
}