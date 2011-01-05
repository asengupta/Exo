namespace Exo.Aspects.Core
{
    public interface IReturnValuePublication
    {
        void Run(object returnValue, string description);
    }
}