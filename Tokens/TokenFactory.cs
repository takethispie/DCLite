using System;
using System.Collections.Generic;
using System.Linq;

namespace rpn_csharp.Tokens
{
    public class TokenFactory
    {
        /// <summary>
        /// Abstract Syntax Tree
        /// </summary>
        public List<IToken> AST;
        private List<IMutator> lookUpMutator;
        private TokenFactory() {
            AST = new List<IToken>();
        }

        public TokenFactory(List<IMutator> lookUpMutator) : this()
        {
            this.lookUpMutator = lookUpMutator;
        }
        public void Create(string item) { 
            try{
                AST = lookUpMutator.First(mut => mut.op.GetValue() == item.ToLower()).Mutate(AST);
            }
            catch (InvalidOperationException invalid)
            {
                AST.Add(new Operand(int.Parse(item)));
            }
        }
    }
}