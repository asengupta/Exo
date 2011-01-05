using System;

namespace Exo.MsmqEndpoint
{
    public class QueueListener
    {
        private readonly DefaultMsmqEndpoint endpoint;
        private readonly TimeSpan timeout;

        public QueueListener(DefaultMsmqEndpoint endpoint, int timeoutInSeconds)
        {
            this.endpoint = endpoint;
            timeout = new TimeSpan(0, 0, timeoutInSeconds);
        }

        public QueueListener(DefaultMsmqEndpoint endpoint) : this(endpoint, 20)
        {
            this.endpoint = endpoint;
        }

        public void Listen()
        {
            endpoint.MessageReceived += delegate(object o)
                                            {
                                                Console.Out.WriteLine("Received {0}", o);
                                                endpoint.BlockingListen(timeout);
                                            };
            endpoint.BlockingListen(timeout);
        }

        public void ListenFor(string message)
        {
            endpoint.ClearListeners();
            endpoint.MessageReceived += (o => Received(o.ToString(), message));
            endpoint.BlockingListen(timeout);
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