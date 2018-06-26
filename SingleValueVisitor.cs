using DCLite.Tokens;

namespace DCLite
{
    public class SingleValueVisitor : IVisitor
    {
        private int value;

        public void Visit(Operand visitee) {
            value = int.Parse(visitee.GetValue());
        }

        public int GetValue() {
            return value;
        }

        public void Visit(IOperator visitee) {
        }
    }
}