using System.Collections.Generic;

namespace Exo.Aspects.Core
{
    public interface IArgumentPublication
    {
        void Run(List<object> arguments, string description);
    }
}