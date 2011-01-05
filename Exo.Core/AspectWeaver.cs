using System;
using CecilBasedWeaver;
using Exo.Attributes;
using Mono.Cecil;
using Mono.Cecil.Rocks;

using System.Linq;

namespace Exo.Core
{
    public class AspectWeaver
    {
        private readonly IAttributeVisitorFactory factory;

        public AspectWeaver(IAttributeVisitorFactory factory)
        {
            this.factory = factory;
        }

        public void Weave(ModuleIO moduleIO, bool isVerbose)
        {
            try
            {
                ModuleDefinition definition = moduleIO.Read();

                if (IsAlreadyWeaved(definition)) return;

                foreach (TypeDefinition typeDefinition in definition.Types)
                {
                    Weave(typeDefinition);
                }

                MarkWeaved(definition);
                moduleIO.Write();
            }
            catch (Exception e)
            {
                if (!isVerbose) return;
                Console.Out.WriteLine(
                    "Error occurred reading/writing assembly {0} and/or its associated pdb...Skipping",
                    moduleIO.AssemblyPath);
                Console.Out.WriteLine(e.Message);
                Console.Out.WriteLine(e);
            }
        }

        private static void MarkWeaved(ModuleDefinition definition)
        {
            definition.CustomAttributes.Add(new CustomAttribute(definition.Import(typeof(IsWeaved).GetConstructor(Type.EmptyTypes))));
        }

        private static bool IsAlreadyWeaved(ModuleDefinition definition)
        {
            return definition.HasCustomAttributes && definition.CustomAttributes.Any(attribute => attribute.Constructor.DeclaringType.FullName == typeof(IsWeaved).FullName);
        }

        private void Weave(TypeDefinition type)
        {
            if (type.IsInterface) return;

            foreach (MethodDefinition method in type.Methods)
            {
                if (method.IsStatic) continue;
                if (!method.HasCustomAttributes) continue;
                if (method.Body == null) Console.Out.WriteLine("Encountered null MethodBody in :{0}", method.FullName);
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