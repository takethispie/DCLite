using System;
using System.Collections.Generic;
using rpn_csharp.Tokens;

namespace rpn_csharp
{
    public class FusionTwoTokenMutator : IMutator
    {
        public IOperator op{get;}
        public FusionTwoTokenMutator(IOperator op)
        {
            this.op = op;
        }

        public List<IToken> Mutate(List<IToken> tokens)
        {
             if(tokens.Count < 2) throw new Exception("not enough tokens !");
            op.childrens = new List<IToken> { tokens[tokens.Count - 2], tokens[tokens.Count - 1]};
            tokens.RemoveAt(tokens.Count - 2);
            tokens.RemoveAt(tokens.Count - 1);
            tokens.Add(op);
            return tokens;
        }
    }
}