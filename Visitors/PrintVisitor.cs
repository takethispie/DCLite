using System;
using System.Collections.Generic;

namespace DCLite
{
    public class PrintVisitor : IVisitor
    {
        public PrintVisitor()
        {
        }

        public void Visit(Add n)
        {
            if(n.Childrens.Count > 2) {
                n.Childrens[0].Accept(this);
                Console.Write(" + ");
                n.Childrens[1].Accept(this);
                Console.Write(" -> ");
                n.Childrens[2].Accept(this);
            }
        }

        public void Visit(Const n)
        {
            Console.Write(n.GetValue());
        }

        public void Visit(Store n)
        {
            if(n.Childrens.Count > 1) {
                n.Childrens[0].Accept(this);
                n.Childrens[1].Accept(this);
            }
        }

        public void Visit(Root n)
        {
            n.Childrens.ForEach(x => x.Accept(this));
            Console.WriteLine("");
        }

        public void Visit(Sub n)
        {
        }

        public void Visit(Mul n)
        {
        }

        public void Visit(Div n)
        {
        }

        public void Visit(Variable n) {
            Console.Write(n.Name);
        }
    }
}