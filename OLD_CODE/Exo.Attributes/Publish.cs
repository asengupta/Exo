using System;

namespace Exo.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true)]
    public class Publish : Attribute
    {
        public PublishTarget Target
        {
            get; set;
        }
    }
}