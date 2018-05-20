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

		public CodeGenerator()
		{
			pc = 1; progStart = -1;
			root = new Root();
		}

		//----- code generation methods -----
		public void Emit(Node node)
		{
			root.Childrens.Add(node);
		}

        public void Decode()
		{
			PrintVisitor visitor = new PrintVisitor();
			root.Accept(visitor);
		}

		public void Compile()
		{
			
		}
	}
}
