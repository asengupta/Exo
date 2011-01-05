using Mono.Cecil;
using Mono.Cecil.Pdb;

namespace CecilBasedWeaver
{
    public class ModuleIO
    {
        private readonly string assemblyPath;

        public string AssemblyPath
        {
            get { return assemblyPath; }
        }

        private ModuleDefinition module;

        public ModuleIO(string assemblyPath)
        {
            this.assemblyPath = assemblyPath;
        }

        public ModuleDefinition Read()
        {
            module = ModuleDefinition.ReadModule(assemblyPath, new ReaderParameters {ReadSymbols = true, SymbolReaderProvider = new PdbReaderProvider()});
            return module;
        }

        public void Write()
        {
            module.Write(assemblyPath, new WriterParameters(){WriteSymbols = true, SymbolWriterProvider = new PdbWriterProvider()});
        }
    }
}