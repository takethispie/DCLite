using System;
using System.Collections.Generic;
using System.IO;

namespace DCLite
{
    public class Store : Node{

        public int constant;
        public string name; 

        public Store(string child1,  int child2) : base()
        {
        }

        public override void Accept(IVisitor v) { v.Visit(this); }
    }
}