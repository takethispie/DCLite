
using DCLite.Tokens;

namespace DCLite
{
    public interface IVisitor
    {
        void Visit(Operand visitee);
        void Visit(IOperator visitee);
    }
}