using System;
using System.Collections.Generic;

namespace rpn_csharp.Tokens
{
    public class Divide : IOperator
    {
        public string value;
        public List<IToken> childrens { get; set; }

        public Divide() : this ("/") {}
        public Divide(string value)
        {
            this.value = value;
        }
        public Divide(IToken left, IToken right) : this("/"){
            this.childrens = new List<IToken>();
            this.childrens.Add(left);
            this.childrens.Add(right);
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
            if (right == 0) throw new ArgumentException("Divide by 0 error!");
            this.value = (left / right).ToString();
        }
    }
}