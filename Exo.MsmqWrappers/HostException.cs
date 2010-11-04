using System;

namespace Exo.MsmqEndpoint
{
    public class HostException : Exception
    {
        public HostException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}