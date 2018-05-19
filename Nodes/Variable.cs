using System;
using System.Collections.Generic;
using System.IO;

namespace DCLite
{
    public class Variable : Node
    {
        private string name;
        private int value;

        public string Name { get => name; set => name = value; }
        public int Value { get => value; set => this.value = value; }

        public Variable(string name) {
            this.name = name;
        }

        public Variable(string name, int value) {
            this.name = name;
            this.value = value;
        }

        public override void Accept(IVisitor v) {
            v.Visit(this);
        }
    }
}