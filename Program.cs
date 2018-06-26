using System;

namespace DCLite {
    class Program {
        static void Main(string[] args) {
            string command = "";
			while (true)  {
				Console.Write (">");
				command = Console.ReadLine();
                if(command.ToUpper() == "QUIT") break;
                long result = Evaluate(command.TrimEnd());
			}
        }

        static long Evaluate(string expression) {
            InterpreterVisitor visitor = new InterpreterVisitor();
            Parser parser = Parser.Instance;
            parser.input = expression;
            parser.visitor = visitor;
            parser.Parse();
            return visitor.GetValue();
        }
    }
}
