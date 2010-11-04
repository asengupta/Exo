using System.Diagnostics;

namespace Exo.Aspects.Core
{
    public interface IStackTracePublication
    {
        void Run(StackTrace trace);
    }
}