using System.Collections.Generic;

namespace rpn_csharp.Tokens
{
    public class Operand : IToken
    {
        private int value;

        public Operand(int value)
        {
            this.value = value;
        }

        public List<IToken> childrens { get; set;}

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            this.childrens = new List<IToken>();
        }

        public string GetValue()
        {
            return this.value.ToString();
        }
    }
}