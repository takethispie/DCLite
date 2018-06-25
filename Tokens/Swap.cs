using System.Collections.Generic;
using rpn_csharp;
using rpn_csharp.Tokens;

namespace DCLite.Tokens
{
    public class Swap : IToken
    {
        public List<IToken> childrens { get; set;} = null;

        public void Accept(IVisitor visitor)
        {
            throw new System.NotImplementedException();
        }

        public string GetValue()
        {
            throw new System.NotImplementedException();
        }
    }
}