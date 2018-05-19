using System;
using System.Collections.Generic;
using System.IO;

namespace DCLite
{
    public class ArithmNodeFactory {
        public Node Create(string op, Node Left, Node Right, Node Target) {
            if(Left == null || Right == null || Target == null || op == "") 
                throw new Exception("Arithm factory arguments error");
            switch(op) {
                case "add": 
                return new Add(Left, Right, Target);

                case "sub":
                return new Sub(Left, Right, Target);

                case "div":
                return new Mul(Left, Right, Target);

                case "mul":
                return new Div(Left, Right, Target);
            }
            return null;
        }
    }
}