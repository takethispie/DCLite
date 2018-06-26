using DCLite.Tokens;
using System;
using System.Collections.Generic;

namespace DCLite
{
    public class InterpreterVisitor : IVisitor
    {
        private int value;

        public void Visit(Operand visitee) {
        }

        public void Visit(IOperator visitee) {
            visitee.Apply();
            Console.WriteLine(visitee.GetValue());
            value = int.Parse(visitee.GetValue());
        }

        public int GetValue() {
            return value;
        }
    }
}