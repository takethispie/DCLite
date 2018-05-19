using System;
using System.Collections.Generic;
using System.IO;

namespace DCLite
{
	public enum Op
	{ 
		ADD, SUB, MUL, DIV, EQU, LSS, GTR, NEG,
		LOAD, LOADG, STO, STOG, CONST,
		JMP, FJMP, READ, WRITE
	}

	public class CodeGenerator
	{

		string[] opcode = { "ADD  ", "SUB  ", "MUL  ", "DIV  ", "EQU  ", "LSS  ", "GTR  ", "NEG  ",
	   "LOAD ", "LOADG", "STO  ", "STOG ", "CONST",
	   "JMP  ", "FJMP ", "READ ", "WRITE" };

		public int progStart;   // address of first instruction of main program
		public int pc;              // program counter
		Node root;
		public Node Current;

		public Dictionary<string, Variable> variables;

		public CodeGenerator()
		{
			pc = 1; progStart = -1;
			root = new Root();
			variables = new Dictionary<string, Variable>();
		}

		//----- code generation methods -----
		public void Emit(Node node)
		{
			
		}

        public void Decode()
		{
			
		}

		public void Compile()
		{
			
		}
	}
}
