namespace CecilBasedWeaver
{
    public interface IAttributeVisitorFactory
    {
        IAttributeVisitor Visitor(string key);
    }
}