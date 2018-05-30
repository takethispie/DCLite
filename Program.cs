using System;

namespace rpn_csharp {
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
            Parser parser = new Parser(expression, visitor);
            parser.Parse();
            return visitor.GetValue();
        }
    }
}
