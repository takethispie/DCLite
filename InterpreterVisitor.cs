using rpn_csharp.Tokens;
using System;
using System.Collections.Generic;

namespace rpn_csharp
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