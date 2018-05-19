﻿using System;
using System.IO;

namespace DCLite
{
    class Program
    {
        static void Main(string[] args)
        {
            const string version = "0.3";
			Scanner sc;
			Parser par;
			string command = "";
			//load DCASM8 Instruction Set Architecture

			Console.Write("DustCat asm lite " + version + Environment.NewLine);
			while (command.ToUpper () != "QUIT") 
			{
				Console.Write (">");
				command = Console.ReadLine();
				string[] cmdSplit = command.Split(' ');

			    if (cmdSplit[0].ToUpper() != "DO") continue;
			    if (File.Exists(cmdSplit[1]))
			    {
			        sc = new Scanner(cmdSplit[1]);
			        par = new Parser(sc);
					par.sytab = new SymbolTable();
					//par.CurrentISA = new DCASM8();

					par.gen = new CodeGenerator();
					par.Parse();
					if (par.errors.count == 0)
						par.gen.Decode();
			    }
			    else
			        Console.WriteLine("File does not exists !");
			}
        }
    }
}