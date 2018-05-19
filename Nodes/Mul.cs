using System;
using System.Collections.Generic;
using System.IO;

namespace DCLite
{
    public class Mul : Node
    {
        public Mul(Node child1, Node child2, Node child3) : base(child1, child2, child3)
        {
        }

        public override void Accept(IVisitor v) { v.Visit(this); }
    }
}