using System;
using System.Collections.Generic;
using rpn_csharp.Tokens;

namespace rpn_csharp
{
    public class Parser
    {
        private static readonly Parser instance = new Parser();

        public string input;
        public List<IToken> output;
        public IVisitor visitor;

        private Parser() {}

        public static Parser Instance {
            get { return instance; }
        }

        public void Parse() {
            output = new List<IToken>();
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