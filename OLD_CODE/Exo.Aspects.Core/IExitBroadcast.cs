namespace Exo.Aspects.Core
{
    public interface IExitBroadcast
    {
        void Run(string description, object enclosingObject);
    }
}