using System.Collections.Generic;
using System;

namespace DCLite.Tokens
{
    public class Substract : IOperator
    {
        public string value;
        public List<IToken> childrens { get; set; }

        public Substract(IToken left, IToken right)
        {
            this.childrens = new List<IToken>();
            this.childrens.Add(left);
            this.childrens.Add(right);
            value = "-";
        }

        public void Accept(IVisitor visitor)
        {
            foreach (IToken token in childrens)
            {
                token.Accept(visitor);
            }
            visitor.Visit(this);
        }

        public string GetValue()
        {
            return this.value;
        }

        public void Apply()
        {
            int left = int.Parse(childrens[0].GetValue());
            int right = int.Parse(childrens[1].GetValue());
            this.value = (left - right).ToString();
        }
    }
}