using System;

namespace Exo.Aspects.Core
{
    public abstract class AbstractPinger
    {
        protected readonly string methodDescription;
        protected readonly Guid guid;

        protected AbstractPinger(string methodDescription, Guid guid)
        {
            this.methodDescription = methodDescription;
            this.guid = guid;
        }

        public abstract void Start();
        public abstract void End();
    }
}