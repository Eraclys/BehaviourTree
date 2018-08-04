namespace BehaviourTree
{
    public interface IVisitor
    {
    }

    public interface IVisitor<in TVisitable> : IVisitor
    {
        void Visit(TVisitable obj);
    }
}
