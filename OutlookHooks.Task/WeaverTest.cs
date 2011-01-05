using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CecilBasedWeaver;
using Exo.Aspects.ComponentQueue;
using Exo.Aspects.Msmq;
using Exo.Aspects.Text;
using Exo.Attributes;
using Exo.Core;
using Exo.Weaves;
using Mono.Cecil;
using NUnit.Framework;
using System.Linq;

namespace OutlookHooks.Task
{
    [TestFixture]
    public class WeaverTest
    {
        private string Read(string fileName, int startLine, int startColumn, int endLine, int endColumn)
        {
            var lines = new List<string>();
            using (var r = new StreamReader(fileName))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            var builder = new StringBuilder();
            lines[startLine - 1] = lines[startLine - 1].Substring(startColumn - 1);
            lines[endLine - 1] = lines[endLine - 1].Substring(0, endColumn - 1);
            for (int i = startLine - 1; i <= endLine - 1; i++)
            {
                builder.AppendLine(lines[i]);
            }

            return builder.ToString();
        }

        [Test]
        public void CanReadLines()
        {
            Read(@"F:\spikes\SampleCecilTestbed\SampleCecilTestbed\Class1.cs", 48, 17, 51, 22);
        }

        [Test]
        public void CanWeaveOutlook()
        {
            var mappings = new Dictionary<string, IWeave>();
            mappings.Add(typeof (Broadcast).FullName, new BroadcastWeave(typeof (MsmqExitBroadcast)));
            mappings.Add(typeof (Publish).FullName,
                         new PublishWeave(typeof (ReturnValueInMemoryPublication), typeof (SelfInMemoryPublication),
                                          typeof (ArgumentInMemoryPublication)));
            mappings.Add(typeof (StackTraceBroadcast).FullName,
                         new StackTraceBroadcastWeave(typeof (TextStackTracePublication)));
            mappings.Add(typeof (Performance).FullName, new ExecutionTimeMonitorWeave(typeof (ExecutionTimeMonitor)));
            mappings.Add(typeof (Ping).FullName, new PingWeave(typeof (MsmqPinger)));
            var weaver = new AspectWeaver(new OutlookAttributeVisitorFactory(mappings));
            weaver.Weave(
                new ModuleIO(
                    @"F:\Projects\IMD\src\Outlook\IMD - Outlook 2003 AddIn\bin\Debug\IMD.AddinProjectDependency.dll"),
                false);
            weaver.Weave(
                new ModuleIO(@"F:\Projects\IMD\src\Outlook\IMD - Outlook 2003 AddIn\bin\Debug\IMD.Outlook.Core.dll"),
                false);
            weaver.Weave(
                new ModuleIO(
                    @"F:\Projects\IMD\src\Outlook\IMD - Outlook 2003 AddIn\bin\Debug\IMD.Outlook.Core.ContactManagement.dll"),
                false);
            weaver.Weave(
                new ModuleIO(@"F:\Projects\IMD\src\Outlook\IMD - Outlook 2003 AddIn\bin\Debug\IMD.Outlook.Services.dll"),
                false);
            weaver.Weave(
                new ModuleIO(
                    @"F:\Projects\IMD\src\Outlook\IMD - Outlook 2003 AddIn\bin\Debug\IMD.Outlook.WrapperImpl.dll"),
                false);
            weaver.Weave(
                new ModuleIO(@"F:\Projects\IMD\src\Outlook\IMD - Outlook 2003 AddIn\bin\Debug\IMD.Outlook.Services.dll"),
                false);
        }

        [Test]
        public void CanWeaveTest()
        {
            var mappings = new Dictionary<string, IWeave>();
            mappings.Add(typeof (Broadcast).FullName, new BroadcastWeave(typeof (TextExitBroadcast)));
            mappings.Add(typeof (Publish).FullName,
                         new PublishWeave(typeof (TextReturnValuePublication), typeof (TextSelfPublication),
                                          typeof (TextArgumentPublication)));
            mappings.Add(typeof (StackTraceBroadcast).FullName,
                         new StackTraceBroadcastWeave(typeof (TextStackTracePublication)));
            mappings.Add(typeof (Performance).FullName, new ExecutionTimeMonitorWeave(typeof (TextExecutionTimeMonitor)));
            mappings.Add(typeof (Ping).FullName, new PingWeave(typeof (TextPinger)));
            mappings.Add(typeof (RemoteDebug).FullName, new DebugWeave(typeof (MsmqBreakpoint)));
            var weaver = new AspectWeaver(new OutlookAttributeVisitorFactory(mappings));
            weaver.Weave(
                new ModuleIO(@"F:\spikes\SampleCecilTestbed\SampleCecilTestbed\bin\Debug\SampleCecilTestbed.exe"), true);
        }

        [Test]
        public void CanWeaveOutlookAddinEntirely()
        {
            var wt = new WeaverTask();
            wt.Verbose = true;
            wt.ExecuteFromUnitTest();
        }

        [Test]
        public void CanWeaveIMDServiceContractDLL()
        {
            var weaver = new AspectWeaver(new OutlookAttributeVisitorFactory());
            weaver.Weave(
                new ModuleIO(
                    @"F:\Projects\IMD\src\Outlook\IMD - Outlook 2003 AddIn\bin\Debug\IMD.Service.Contract.dll"),
                false);
        }

        [Test, Ignore]
        public void DebugWeave()
        {
            var weaver = new AspectWeaver(new OutlookAttributeVisitorFactory());
            weaver.Weave(
                new ModuleIO(
                    @"C:\Users\prateekk\Documents\Visual Studio 2008\Projects\TrialProject\TrialProject\bin\Debug\TrialProject.exe"),
                false);
        }


        [Test, Ignore]
        public void CanAddIdentifyAttributesBeforeWeaving()
        {
            var module = new ModuleIO(@"D:\Projects\BCG_Mailing\IMD\weaver\TestExoProject\bin\debug\TestExoProject.dll");
            var definition = module.Read();
            CustomAttribute ca = new CustomAttribute (definition.Import (typeof (Publish).GetConstructor (Type.EmptyTypes)));
            definition.CustomAttributes.Add(ca);
            definition.Write(@"D:\Projects\BCG_Mailing\IMD\weaver\TestExoProject\bin\debug\NewTestExoProject.dll");

            var newModule = new ModuleIO(@"D:\Projects\BCG_Mailing\IMD\weaver\TestExoProject\bin\debug\NewTestExoProject.dll");
            var newDef = newModule.Read();
            Assert.That(newDef.HasCustomAttributes);
            Assert.That(newDef.CustomAttributes.Any(attribute => attribute.Constructor.DeclaringType.FullName == typeof(Publish).FullName));
        }
    }

}