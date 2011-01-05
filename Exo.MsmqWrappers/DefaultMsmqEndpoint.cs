using System;
using System.Messaging;

namespace Exo.MsmqEndpoint
{
    public class DefaultMsmqEndpoint
    {
        public delegate void Received(object message);
        private const string Location = "Private$";
        private readonly MessageQueue queue;

        public DefaultMsmqEndpoint(string queueName)
        {
            queue = MessageQueue.Exists(string.Format(@".\{1}\{0}", queueName, Location))
                        ? new MessageQueue(string.Format(@".\{1}\{0}", queueName, Location))
                        : MessageQueue.Create(string.Format(@".\{1}\{0}", queueName, Location));
            queue.Formatter = new XmlMessageFormatter(new[] {typeof (string)});
            queue.UseJournalQueue = true;
        }

        public event Received MessageReceived;

        public object BlockingListen()
        {
            return BlockingListen(MessageQueue.InfiniteTimeout);
         }

        public object BlockingListen(TimeSpan timeout)
        {
            Message message = queue.Receive(timeout);
            if (message == null) return null;
            return message.Body;
         }

        public void ClearListeners()
        {
            MessageReceived = null;
        }

        public void Listen()
        {
            queue.ReceiveCompleted += ReceivedMessage;
            queue.BeginReceive();
        }

        public void Purge()
        {
            queue.Purge();
        }

        private void ReceivedMessage(object sender, ReceiveCompletedEventArgs e)
        {
            MessageReceived(e.Message.Body);
            queue.BeginReceive();
        }

        public void Send(string message)
        {
            queue.Send(message);
        }
    }
}