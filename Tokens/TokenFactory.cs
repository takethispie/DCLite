using System;
using System.Collections.Generic;

namespace rpn_csharp.Tokens
{
    public class TokenFactory
    {
        /// <summary>
        /// Abstract Syntax Tree
        /// </summary>
        public List<IToken> AST;

        public TokenFactory() {
            AST = new List<IToken>();
        }

        public void Create(string item) { 
            //idea: use a lookup table with the item as id and 
            //a command (=> we use the command pattern) as value
            // execute the command using AST as parameter 
            switch (item.ToLower())
            {
                case "+":
                if(AST.Count < 2) throw new Exception("not enough tokens !");
                Add add = new Add(AST[AST.Count - 2], AST[AST.Count - 1]);
                AST.RemoveAt(AST.Count - 2);
                AST.RemoveAt(AST.Count - 1);
                AST.Add(add);
                break;

                case "-":
                if(AST.Count < 2) throw new Exception("not enough tokens !");
                Substract sub = new Substract(AST[AST.Count - 2], AST[AST.Count - 1]);
                AST.RemoveAt(AST.Count - 2);
                AST.RemoveAt(AST.Count - 1);
                AST.Add(sub);
                break;

                case "*":
                if(AST.Count < 2) throw new Exception("not enough tokens !");
                Multiply mul = new Multiply(AST[AST.Count - 2], AST[AST.Count - 1]);
                AST.RemoveAt(AST.Count - 2);
                AST.RemoveAt(AST.Count - 1);
                AST.Add(mul);
                break;

                case "/":
                if(AST.Count < 2) throw new Exception("not enough tokens !");
                Divide div = new Divide(AST[AST.Count - 2], AST[AST.Count - 1]);
                AST.RemoveAt(AST.Count - 2);
                AST.RemoveAt(AST.Count - 1);
                AST.Add(div);
                break;

                case "pop":
                if(AST.Count == 0) throw new Exception("the stack is empty !");
                AST.RemoveAt(AST.Count - 1);
                break;

                case "dup":
                if(AST.Count == 0) throw new Exception("the stack is empty !");
                AST.Add(AST[AST.Count - 1]);
                break;

                case "swap":
                if(AST.Count < 2) throw new Exception("need at least 2 item to do a swap !");
                IToken left = AST[AST.Count - 2];
                IToken right = AST[AST.Count - 1];
                AST.RemoveAt(AST.Count - 2);
                AST.RemoveAt(AST.Count - 1);
                AST.Add(right);
                AST.Add(left);
                break;
                
                default:
                //need to handle error with int.tryparse 
                AST.Add(new Operand(int.Parse(item)));
                break;
            }
        }
    }
}