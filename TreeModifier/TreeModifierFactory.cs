using System.Collections.Generic;

namespace DCLite.TreeModifier
{
    public class TreeModifierFactory
    {
        Dictionary<string, ITreeModifier> look;
         
        public TreeModifierFactory()
        {
            look = new Dictionary<string, ITreeModifier>{
                {"swap", new Swap()},
                {"pop", new Pop()},
                {"dup", new Duplicate()}
            };
        }

        public bool Contains(string item) => look.ContainsKey(item);
        
        public ITreeModifier Create(string item) => look[item];
    }
}