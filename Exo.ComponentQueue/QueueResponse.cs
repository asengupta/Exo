namespace Exo.EventBus
{
    public class QueueResponse
    {
        public QueueResponse(string message)
        {
            Message = message;
        }

        public string Message
        {
            get; set;
        }
    }
}