
using System;

namespace DCLite {



public class Parser {
	public const int _EOF = 0;
	public const int _ident = 1;
	public const int _number = 2;
	public const int maxT = 13;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;

public CodeGenerator gen;
  public SymbolTable sytab;
  
/*--------------------------------------------------------------------------*/


	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}


	bool WeakSeparator(int n, int syFol, int repFol) {
		int kind = la.kind;
		if (kind == n) {Get(); return true;}
		else if (StartOf(repFol)) {return false;}
		else {
			SynErr(n);
			while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
				Get();
				kind = la.kind;
			}
			return StartOf(syFol);
		}
	}

	
	void Ident(out string name) {
		Expect(3);
		Expect(1);
		name = t.val; 
	}

	void LIdent(out string name) {
		Expect(4);
		Expect(1);
		name = t.val; 
	}

	void DCasm() {
		string name; 
		Expect(5);
		while (StartOf(1)) {
			if (StartOf(2)) {
				Arithm();
			} else if (la.kind == 11 || la.kind == 12) {
				Branch();
			} else {
				Get();
				LIdent(out name);
				sytab.Add(new Variable(name, 0)); 
			}
		}
		Expect(0);
	}

	void Arithm() {
		string name1; string name2; string name3; string op; Node child1 = null, child2 = null, child3 = null; 
		if (la.kind == 7) {
			Get();
		} else if (la.kind == 8) {
			Get();
		} else if (la.kind == 9) {
			Get();
		} else if (la.kind == 10) {
			Get();
			op = t.val; 
			if (la.kind == 3) {
				Ident(out name1);
				child1 = new Variable(name1); 
			} else if (la.kind == 2) {
				Get();
				child1 = new Const(int.Parse(t.val)); 
			} else SynErr(14);
			if (la.kind == 3) {
				Ident(out name2);
				child2 = new Variable(name2); 
			} else if (la.kind == 2) {
				Get();
				child2 = new Const(int.Parse(t.val)); 
			} else SynErr(15);
			Ident(out name3);
			child3 = new Variable(name3); gen.Emit(new ArithmNodeFactory().Create(op, child1, child2, child3)); 
		} else SynErr(16);
	}

	void Branch() {
		string name; 
		if (la.kind == 11) {
			Get();
			LIdent(out name);
			
		} else if (la.kind == 12) {
			Get();
			LIdent(out name);
			
		} else SynErr(17);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		DCasm();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_T,_x, _x,_x,_x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
	public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

	public virtual void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "ident expected"; break;
			case 2: s = "number expected"; break;
			case 3: s = "\"$\" expected"; break;
			case 4: s = "\"#\" expected"; break;
			case 5: s = "\"program\" expected"; break;
			case 6: s = "\"var\" expected"; break;
			case 7: s = "\"add\" expected"; break;
			case 8: s = "\"sub\" expected"; break;
			case 9: s = "\"mul\" expected"; break;
			case 10: s = "\"div\" expected"; break;
			case 11: s = "\"goto\" expected"; break;
			case 12: s = "\"label\" expected"; break;
			case 13: s = "??? expected"; break;
			case 14: s = "invalid Arithm"; break;
			case 15: s = "invalid Arithm"; break;
			case 16: s = "invalid Arithm"; break;
			case 17: s = "invalid Branch"; break;

			default: s = "error " + n; break;
		}
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}

	public virtual void SemErr (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}
	
	public virtual void SemErr (string s) {
		errorStream.WriteLine(s);
		count++;
	}
	
	public virtual void Warning (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
	}
	
	public virtual void Warning(string s) {
		errorStream.WriteLine(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}
}