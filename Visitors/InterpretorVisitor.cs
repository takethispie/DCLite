using System.Collections.Generic;

namespace DCLite.Visitors
{
    public class InterpretorVisitor : IVisitor
    {
        private Dictionary<string, int> variables;

        public InterpretorVisitor() {
			variables = new Dictionary<string, int>();            
        }

        public void Visit(Root n)
        {
            n.Childrens.ForEach(x => x.Accept(this));
        }

        public void Visit(Store n)
        {
            if(variables.ContainsKey(n.name)) variables[n.name] = n.constant;
            else variables.Add(n.name, n.constant);
        }

        public void Visit(Const n)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Add n)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Sub n)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Mul n)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Div n)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Variable n)
        {
            throw new System.NotImplementedException();
        }
    }
}