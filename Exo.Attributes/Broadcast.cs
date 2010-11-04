using System;

namespace Exo.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Broadcast : Attribute
    {
        public string Description
        {
            get; set;
        }
    }
}