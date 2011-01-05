namespace Exo.Aspects.Core
{
    public interface IBreakPoint
    {
//        void Activate(string documentURL);
        void Activate(int startLine, int startColumn, int endLine, int endColumn, string documentURL);
    }
}