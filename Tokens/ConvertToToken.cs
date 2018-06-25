using System;
using System.Collections.Generic;
using rpn_csharp.Tokens;

namespace DCLite.Tokens
{
    public class ConvertToToken
    {
        private Dictionary<string, Func<IToken,IToken, IToken>> lookupOperatorTable;
        public ConvertToToken() {
            lookupOperatorTable = new Dictionary<string, Func<IToken, IToken, IToken>>{
                {"+", (token1, token2) => new Add(token1, token2)},
                {"-", (token1, token2) => new Substract(token1, token2)},
                {"*", (token1, token2) => new Multiply(token1, token2)},
                {"/", (token1, token2) => new Divide(token1, token2)},
                {"swap", (_, __) => new Swap()},
                {"pop", (_, __)  => new Pop()},
                {"dup", (_, __)  => new Duplicate()}
            };
        }

        public IToken LookUp(string item, IToken token1, IToken token2) {
            return lookupOperatorTable[item].Invoke(token1, token2);
        }

    }
}