using System.Collections.Generic;

namespace rpn_csharp.Tokens
{
    public interface IToken
    {
        void Accept(IVisitor visitor);
        List<IToken> childrens { get; set; }
        string GetValue();
    }
}