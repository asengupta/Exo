using System;
using CecilBasedWeaver;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Exo.Core
{
    public class BroadcastAspectWeaver
    {
        private readonly IAttributeVisitorFactory factory;

        public BroadcastAspectWeaver(IAttributeVisitorFactory factory)
        {
            this.factory = factory;
        }

        public void Weave(ModuleIO moduleIO, bool isVerbose)
        {
            try
            {
                ModuleDefinition definition = moduleIO.Read();
                foreach (TypeDefinition typeDefinition in definition.Types)
                {
                    Weave(typeDefinition);
                }
                moduleIO.Write();
            }
            catch (Exception e)
            {
                if (!isVerbose) return;
                Console.Out.WriteLine(
                    "Error occurred reading/writing assembly {0} and/or its associated pdb...Skipping",
                    moduleIO.AssemblyPath);
                Console.Out.WriteLine(e.Message);
            }
        }

        private void Weave(TypeDefinition type)
        {
            foreach (MethodDefinition method in type.Methods)
            {
                if (method.IsStatic) continue;
                if (!method.HasCustomAttributes) continue;
                method.Body.SimplifyMacros();
                foreach (CustomAttribute attribute in method.CustomAttributes)
                {
                    string attributeName = attribute.Constructor.DeclaringType.FullName;
                    IAttributeVisitor weave = factory.Visitor(attributeName);
                    weave.Visit(new WeavableMethodDefinition(method), attribute);
                }
                method.Body.OptimizeMacros();
            }

            foreach (PropertyDefinition property in type.Properties)
            {
                if (!property.HasCustomAttributes) continue;
                foreach (CustomAttribute attribute in property.CustomAttributes)
                {
                    IAttributeVisitor weave = factory.Visitor(attribute.Constructor.DeclaringType.FullName);
                    weave.Visit(new WeavablePropertyDefinition(property), attribute);
                }
            }
        }
    }
}