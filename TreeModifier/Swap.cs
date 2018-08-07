using System;
using System.Collections.Generic;
using DCLite.Tokens;

namespace DCLite.TreeModifier
{
    public class Swap : ITreeModifier
    {
        public List<IToken> Modify(List<IToken> tree)
        {
            if(tree.Count < 2) throw new Exception("need at least 2 item to do a swap !");
            IToken left = tree[tree.Count - 2];
            IToken right = tree[tree.Count - 1];
            tree.RemoveAt(tree.Count - 2);
            tree.RemoveAt(tree.Count - 1);
            tree.Add(right);
            tree.Add(left);
            return tree;
        }
    }
}