using System.Collections.Generic;

namespace DCLite.Tokens
{
    public interface IToken
    {
        void Accept(IVisitor visitor);
        List<IToken> childrens { get; set; }
        string GetValue();
    }
}