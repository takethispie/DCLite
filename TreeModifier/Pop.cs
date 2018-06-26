using System;
using System.Collections.Generic;
using DCLite.Tokens;

namespace DCLite.TreeModifier
{
    public class Pop : ITreeModifier
    {
        public List<IToken> Modify(List<IToken> tree)
        {
            if(tree.Count == 0) throw new Exception("the stack is empty !");
            tree.RemoveAt(tree.Count - 1);
            return tree;
        }
    }
}