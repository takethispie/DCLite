using System.Collections.Generic;

namespace DCLite
{
    public class SymbolTable
    {
        private Dictionary<string, Variable> variables;

        public void Add(Variable var) {
            if(variables.ContainsKey(var.Name)) return;
            variables.Add(var.Name, var);
        }

        public bool Exists(string name) {
            return variables.ContainsKey(name);
        }
    }
}