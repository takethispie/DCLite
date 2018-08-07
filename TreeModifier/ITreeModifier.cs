using System.Collections.Generic;
using DCLite.Tokens;

namespace DCLite.TreeModifier
{
    public interface ITreeModifier
    {
        List<IToken> Modify(List<IToken> tree);
    }
}