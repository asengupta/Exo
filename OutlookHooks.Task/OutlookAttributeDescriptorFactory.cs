using System.Collections.Generic;
using CecilBasedWeaver;
using Exo.Attributes;

namespace OutlookHooks.Task
{
    public class OutlookAttributeDescriptorFactory : IAttributeVisitorFactory
    {
        private readonly IDictionary<string, IAttributeVisitor> weaves;

        public OutlookAttributeDescriptorFactory(IDictionary<string, IAttributeVisitor> weaves)
        {
            this.weaves = weaves;
        }

        public OutlookAttributeDescriptorFactory() : this(new Dictionary<string, IAttributeVisitor>
                                                              {
                                                                  {typeof (Broadcast).FullName, new AttributeDescriptor()},
                                                                  {typeof (Publish).FullName, new AttributeDescriptor()},
                                                                  {typeof (StackTraceBroadcast).FullName, new AttributeDescriptor()}
                                                              })
        {
        }

        public IAttributeVisitor Visitor(string key)
        {
            if (weaves.ContainsKey(key)) return weaves[key];
            return new NullVisitor();
        }
    }
}