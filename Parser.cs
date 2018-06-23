using System;
using System.Collections.Generic;
using rpn_csharp.Tokens;

namespace rpn_csharp
{
    public class Parser
    {
        public string input;
        public List<IToken> output;
        public IVisitor visitor;

        public Parser(string input, IVisitor visitor) {
            this.input = input;
            this.output = new List<IToken>();
            this.visitor = visitor;
        }

        public void Parse() {
            List<string> items = new List<string>(this.input.Split(" "));
            int singleValue = 0;
            try {
                if (items.Count == 1) {
                    singleValue = int.Parse(items[0]);
                    IToken token = new Constant(singleValue);
                    token.Accept(visitor);
                    return;
                }
            } catch (Exception e) {

            }

            TokenFactory factory = new TokenFactory();
            foreach (String item in items) {
                factory.Create(item);
            }
            this.output = factory.AST;
            if (this.output.Count == 1 && this.output[0].GetType() == typeof(Operand)) 
                visitor = new SingleValueVisitor();
            foreach(IToken token in this.output) {
                token.Accept(visitor);
            }
        }
    }
}