using System;
using Mono.Cecil;

namespace CecilBasedWeaver
{
    public class UnsupportedPropertyFormException : Exception
    {
        public UnsupportedPropertyFormException(MemberReference definition) : base("Properties without getters are not supported yet - " + definition.FullName)
        {
        }
    }
}