using System;
using System.Collections.Generic;

namespace DCLite.Tokens
{
    public class Divide : IOperator
    {
        public string value;
        public List<IToken> childrens { get; set; }

        public Divide(IToken left, IToken right) {
            this.childrens = new List<IToken>();
            this.childrens.Add(left);
            this.childrens.Add(right);
            value = "/";
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