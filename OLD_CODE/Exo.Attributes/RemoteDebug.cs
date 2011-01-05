using System;

namespace Exo.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class RemoteDebug : Attribute
    {
    }
}