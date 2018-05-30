
using rpn_csharp.Tokens;

namespace rpn_csharp
{
    public interface IVisitor
    {
        void Visit(Operand visitee);
        void Visit(IOperator visitee);
    }
}