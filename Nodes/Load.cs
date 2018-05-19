using System;
using System.Collections.Generic;
using System.IO;

namespace DCLite
{
    public class Load : Node{
        public Load(Node child1, Node child2) : base(child1, child2)
        {
        }

        public override void Accept(IVisitor v) { v.Visit(this); }
    }
}