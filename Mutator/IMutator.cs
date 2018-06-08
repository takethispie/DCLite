using System.Collections.Generic;
using rpn_csharp.Tokens;

namespace rpn_csharp
{
    public interface IMutator
    {
        IOperator op {get;}
        List<IToken> Mutate(List<IToken> tokens);
    }
}