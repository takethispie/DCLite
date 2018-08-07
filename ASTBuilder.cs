using System;
using System.Collections.Generic;
using DCLite.Tokens;
using DCLite.TreeModifier;

namespace DCLite
{
    public class ASTBuilder
    {
        /// <summary>
        /// Abstract Syntax Tree
        /// </summary>
        public List<IToken> AST;
        public TokenFactory tokenFactory;

        public TreeModifierFactory treeModifierFactory;
        
        public Dictionary<Type, Action> process;
        public ASTBuilder(TokenFactory token, TreeModifierFactory tree) {
            AST = new List<IToken>();
            tokenFactory = token;
            treeModifierFactory = tree;
        }

        public void Create(string item) { 

            string param = item.ToLower();
            if(tokenFactory.Contains(param))
            {
                IToken token = tokenFactory.Create(param, AST[AST.Count - 2], AST[AST.Count - 1]);
                if(AST.Count < 2) throw new Exception("not enough tokens !");
                AST.RemoveAt(AST.Count - 2);
                AST.RemoveAt(AST.Count - 1);
                AST.Add(token);
                return;
            }

            if(treeModifierFactory.Contains(param))
            {
                ITreeModifier treeModif = treeModifierFactory.Create(param);
                AST = treeModif.Modify(AST);
                return;
            }
            //need to handle error with int.tryparse 
            try {
                AST.Add(new Operand(int.Parse(item)));
            } catch(Exception) {
                Console.Clear();
                Console.WriteLine("erreur le charactère rentré n'est pas reconnu comme symbole valide !");
                Console.WriteLine("appuyez sur entrée pour recommencer...");
                Console.ReadLine();
                Console.Clear();
                this.AST = new List<IToken>();
            }
        }
    }
}