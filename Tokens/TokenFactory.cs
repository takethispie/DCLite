using System;
using System.Collections.Generic;
using DCLite.Tokens;

namespace rpn_csharp.Tokens
{
    public class TokenFactory
    {
        /// <summary>
        /// Abstract Syntax Tree
        /// </summary>
        public List<IToken> AST;
        public ConvertToToken converter;
        public Dictionary<Type, Action> process;
        public TokenFactory() {
            AST = new List<IToken>();
        }

        private void UseInstruction(string item)
        {
            IToken token = converter.LookUp(item.ToLower(), AST[AST.Count - 2], AST[AST.Count - 1]);
            //process[typeof(IOperator)].Invoke();

            if(token is IOperator)
            {
                if(AST.Count < 2) throw new Exception("not enough tokens !");
                AST.RemoveAt(AST.Count - 2);
                AST.RemoveAt(AST.Count - 1);
                AST.Add(token);
                return;
            }
            if(token is Pop)
            {
               if(AST.Count == 0) throw new Exception("the stack is empty !");
                AST.RemoveAt(AST.Count - 1);
                return;
            } 
             if (token is Duplicate)
            {
                if(AST.Count == 0) throw new Exception("the stack is empty !");
                AST.Add(AST[AST.Count - 1]);
                return;
            }
            if (token is Swap)
            {
                if(AST.Count < 2) throw new Exception("need at least 2 item to do a swap !");
                IToken left = AST[AST.Count - 2];
                IToken right = AST[AST.Count - 1];
                AST.RemoveAt(AST.Count - 2);
                AST.RemoveAt(AST.Count - 1);
                AST.Add(right);
                AST.Add(left);
                return;
            }
            //need to handle error with int.tryparse 
            try {
                AST.Add(new Operand(int.Parse(item)));
            } catch(Exception ex) {
                Console.Clear();
                Console.WriteLine("erreur le charactère rentré n'est pas reconnu comme symbole valide !");
                Console.WriteLine("appuyez sur entrée pour recommencer...");
                Console.ReadLine();
                Console.Clear();
                this.AST = new List<IToken>();
            }
        }
        
        public void Create(string item) { 
            //idea: use a lookup table with the item as id and 
            //a command (=> we use the command pattern) as value
            // execute the command using AST as parameter 
            
            string param = item.ToLower();
            UseInstruction(item);
        }
    }
}