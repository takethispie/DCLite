using System;
using System.Collections.Generic;

namespace DCLite.Tokens
{
    public class TokenFactory
    {
        private Dictionary<string, Func<IToken,IToken, IToken>> lookupOperatorTable;
        public TokenFactory() {
            lookupOperatorTable = new Dictionary<string, Func<IToken, IToken, IToken>>{
                {"+", (token1, token2) => new Add(token1, token2)},
                {"-", (token1, token2) => new Substract(token1, token2)},
                {"*", (token1, token2) => new Multiply(token1, token2)},
                {"/", (token1, token2) => new Divide(token1, token2)}
            };
        }
        
        public bool Contains(string item) => lookupOperatorTable.ContainsKey(item);

        public IToken Create(string item, IToken left, IToken right) => lookupOperatorTable[item].Invoke(left, right);
    }
}