using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exo.Attributes
{
    [AttributeUsage(AttributeTargets.Module, AllowMultiple = true)]
    public class IsWeaved : Attribute
    {
    }
}

