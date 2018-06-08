using System;
using System.Collections.Generic;
using rpn_csharp.Tokens;

namespace rpn_csharp
{
    public class SwapTwoTokenMutator : IMutator
    {
        public IOperator op{get;}
        public SwapTwoTokenMutator(IOperator op)
        {
            this.op = op;
        }

        public List<IToken> Mutate(List<IToken> tokens)
        {
             if(tokens.Count < 2) throw new Exception("need at least 2 item to do a swap !");
            IToken left = tokens[tokens.Count - 2];
            IToken right = tokens[tokens.Count - 1];
            tokens.RemoveAt(tokens.Count - 2);
            tokens.RemoveAt(tokens.Count - 1);
            tokens.Add(right);
            tokens.Add(left);
            return tokens;
        }
    }
}