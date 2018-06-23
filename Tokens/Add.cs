using System;
using System.Collections.Generic;

namespace rpn_csharp.Tokens
{
    public class Add : IOperator
    {
        public string value;
        public List<IToken> childrens { get; set; }

        public Add(IToken left, IToken right) {
            this.childrens = new List<IToken>();
            this.childrens.Add(left);
            this.childrens.Add(right);
            value = "+";
        }

        public void Accept(IVisitor visitor) {
            foreach (IToken token in childrens) {
                token.Accept(visitor);
            }
            visitor.Visit(this);
        }

        public string GetValue() {
            return this.value;
        }

        public void Apply() {
            int left = int.Parse(childrens[0].GetValue());
            int right = int.Parse(childrens[1].GetValue());
            this.value = (left + right).ToString();
        }
    }
}