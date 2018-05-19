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
            if(n.Childrens.Count > 1) {
                n.Childrens[0].Accept(this);
                Console.WriteLine("+");
                n.Childrens[1].Accept(this);
            }
        }

        public void Visit(Const n)
        {
            Console.WriteLine((n).GetValue());
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
        }

        public void Visit(Load n)
        {
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

        public void VisitRead(Node n)
        {
        }

        public void VisitWrite(Node n)
        {
        }

        public void Visit(Variable n) {
            
        }
    }
}