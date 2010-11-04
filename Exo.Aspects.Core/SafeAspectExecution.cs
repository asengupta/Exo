using System;
using System.Diagnostics;

namespace Exo.Aspects.Core
{
    public class SafeAspectExecution
    {
        public void Run(Action a)
        {
            try
            {
                a();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}