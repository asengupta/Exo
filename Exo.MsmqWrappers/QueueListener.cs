using System;

namespace Exo.MsmqEndpoint
{
    public class QueueListener
    {
        private readonly DefaultMsmqEndpoint endpoint;
        private readonly TimeSpan timeout;
        public string LastMessage;

        public QueueListener(DefaultMsmqEndpoint endpoint, int timeoutInSeconds)
        {
            this.endpoint = endpoint;
            timeout = new TimeSpan(0, 0, timeoutInSeconds);
        }

        public QueueListener(DefaultMsmqEndpoint endpoint) : this(endpoint, 60)
        {
            this.endpoint = endpoint;
        }

        public string Listen()
        {
            return endpoint.BlockingListen(timeout).ToString();
        }

        public void ListenFor(string expected)
        {
            var message = endpoint.BlockingListen(timeout).ToString();
            while( message != expected)
                message = endpoint.BlockingListen(timeout).ToString();
         }

        private void Received(string actual, string expected)
        {
            if (IsError(actual)) throw new HostException(actual);
            if (!expected.Equals(actual))
            {
                endpoint.BlockingListen(timeout);
            }
        }

        private bool IsError(string message)
        {
            return false;
        }
    }
}