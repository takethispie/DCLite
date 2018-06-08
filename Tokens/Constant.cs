using System.Collections.Generic;

namespace rpn_csharp.Tokens
{
    public class Constant : IOperator
    {
        public string value;
        public List<IToken> childrens { get; set; }
        
        public Constant(string value)
        {
            this.value = value;
        }
        public Constant(int value) {
            this.value = value.ToString();
            this.childrens = new List<IToken>();
        }


        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }

        public string GetValue() {
            return this.value;
        }

        public void Apply() {
            
        }
    }
}