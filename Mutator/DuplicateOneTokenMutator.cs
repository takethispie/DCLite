using System;
using System.Collections.Generic;
using rpn_csharp.Tokens;

namespace rpn_csharp
{
    public class DuplicateOneTokenMutator : IMutator
    {
        public IOperator op{get;}
        public DuplicateOneTokenMutator(IOperator op)
        {
            this.op = op;
        }

        public List<IToken> Mutate(List<IToken> tokens)
        {
            if(tokens.Count == 0) throw new Exception("the stack is empty !");
            tokens.Add(tokens[tokens.Count - 1]);
            return tokens;
        }
    }
}