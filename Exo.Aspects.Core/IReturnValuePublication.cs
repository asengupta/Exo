namespace Exo.Aspects.Core
{
    public interface IReturnValuePublication
    {
        void Run(object enclosingObject, object returnValue);
    }
}