
using System;
using System.IO;
using System.Collections.Generic;

namespace DCLite {
	public class Scanner {
		const char EOL = '\n';
		const int eofSym = 0; /* pdt */
		const int maxT = 11;
		const int noSym = 11;

		public Buffer buffer; // scanner buffer
		
		Token t;          // current token
		int ch;           // current input character
		int pos;          // byte position of current character
		int charPos;      // position by unicode characters starting with 0
		int col;          // column number of current character
		int line;         // line number of current character
		int oldEols;      // EOLs that appeared in a comment;
		static readonly Dictionary<int, int> start; // maps first token character to start state

		Token tokens;     // list of tokens already peeked (first token is a dummy)
		Token pt;         // current peek token
		
		char[] tval = new char[128]; // text of current token
		int tlen;         // length of current token
		
		static Scanner() {
			start = new Dictionary<int, int>(128);
			for (int i = 65; i <= 90; ++i) start[i] = 1;
			for (int i = 97; i <= 122; ++i) start[i] = 1;
			for (int i = 48; i <= 57; ++i) start[i] = 2;
			start[123] = 3; 
			start[125] = 4; 
			start[Buffer.EOF] = -1;
		}
		
		public Scanner (string fileName) {
			try {
				Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				buffer = new Buffer(stream, false);
				Init();
			} catch (IOException) {
				throw new FatalError("Cannot open file " + fileName);
			}
		}
		
		public Scanner (Stream s) {
			buffer = new Buffer(s, true);
			Init();
		}
		
		void Init() {
			pos = -1; line = 1; col = 0; charPos = -1;
			oldEols = 0;
			NextCh();
			if (ch == 0xEF) { // check optional byte order mark for UTF-8
				NextCh(); int ch1 = ch;
				NextCh(); int ch2 = ch;
				if (ch1 != 0xBB || ch2 != 0xBF) {
					throw new FatalError(String.Format("illegal byte order mark: EF {0,2:X} {1,2:X}", ch1, ch2));
				}
				buffer = new UTF8Buffer(buffer); col = 0; charPos = -1;
				NextCh();
			}
			pt = tokens = new Token();
		}
		
		void NextCh() {
			if (oldEols > 0) { ch = EOL; oldEols--; } 
			else {
				pos = buffer.Pos;
				// buffer reads unicode chars, if UTF8 has been detected
				ch = buffer.Read(); col++; charPos++;
				// replace isolated '\r' by '\n' in order to make
				// eol handling uniform across Windows, Unix and Mac
				if (ch == '\r' && buffer.Peek() != '\n') ch = EOL;
				if (ch == EOL) { line++; col = 0; }
			}

		}

		void AddCh() {
			if (tlen >= tval.Length) {
				char[] newBuf = new char[2 * tval.Length];
				Array.Copy(tval, 0, newBuf, 0, tval.Length);
				tval = newBuf;
			}
			if (ch != Buffer.EOF) {
				tval[tlen++] = (char) ch;
				NextCh();
			}
		}



		bool Comment0() {
			int level = 1, pos0 = pos, line0 = line, col0 = col, charPos0 = charPos;
			NextCh();
			if (ch == '/') {
				NextCh();
				for(;;) {
					if (ch == 10) {
						level--;
						if (level == 0) { oldEols = line - line0; NextCh(); return true; }
						NextCh();
					} else if (ch == Buffer.EOF) return false;
					else NextCh();
				}
			} else {
				buffer.Pos = pos0; NextCh(); line = line0; col = col0; charPos = charPos0;
			}
			return false;
		}

		bool Comment1() {
			int level = 1, pos0 = pos, line0 = line, col0 = col, charPos0 = charPos;
			NextCh();
			if (ch == '*') {
				NextCh();
				for(;;) {
					if (ch == '*') {
						NextCh();
						if (ch == '/') {
							level--;
							if (level == 0) { oldEols = line - line0; NextCh(); return true; }
							NextCh();
						}
					} else if (ch == '/') {
						NextCh();
						if (ch == '*') {
							level++; NextCh();
						}
					} else if (ch == Buffer.EOF) return false;
					else NextCh();
				}
			} else {
				buffer.Pos = pos0; NextCh(); line = line0; col = col0; charPos = charPos0;
			}
			return false;
		}


		void CheckLiteral() {
			switch (t.val) {
				case "program": t.kind = 3; break;
				case "store": t.kind = 5; break;
				case "add": t.kind = 7; break;
				case "sub": t.kind = 8; break;
				case "mul": t.kind = 9; break;
				case "div": t.kind = 10; break;
				default: break;
			}
		}

		Token NextToken() {
			while (ch == ' ' ||
				ch >= 9 && ch <= 10 || ch == 13
			) NextCh();
			if (ch == '/' && Comment0() ||ch == '/' && Comment1()) return NextToken();
			int recKind = noSym;
			int recEnd = pos;
			t = new Token();
			t.pos = pos; t.col = col; t.line = line; t.charPos = charPos;
			int state;
			state = start.ContainsKey(ch) ? start[ch] : 0;
			tlen = 0; AddCh();
			
			switch (state) {
				case -1: { t.kind = eofSym; break; } // NextCh already done
				case 0: {
					if (recKind != noSym) {
						tlen = recEnd - t.pos;
						SetScannerBehindT();
					}
					t.kind = recKind; break;
				} // NextCh already done
				case 1:
					recEnd = pos; recKind = 1;
					if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z') {AddCh(); goto case 1;}
					else {t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
				case 2:
					recEnd = pos; recKind = 2;
					if (ch >= '0' && ch <= '9') {AddCh(); goto case 2;}
					else {t.kind = 2; break;}
				case 3:
					{t.kind = 4; break;}
				case 4:
					{t.kind = 6; break;}

			}
			t.val = new String(tval, 0, tlen);
			return t;
		}
		
		private void SetScannerBehindT() {
			buffer.Pos = t.pos;
			NextCh();
			line = t.line; col = t.col; charPos = t.charPos;
			for (int i = 0; i < tlen; i++) NextCh();
		}
		
		// get the next token (possibly a token already seen during peeking)
		public Token Scan () {
			if (tokens.next == null) {
				return NextToken();
			} else {
				pt = tokens = tokens.next;
				return tokens;
			}
		}

		// peek for the next token, ignore pragmas
		public Token Peek () {
			do {
				if (pt.next == null) {
					pt.next = NextToken();
				}
				pt = pt.next;
			} while (pt.kind > maxT); // skip pragmas
		
			return pt;
		}

		// make sure that peeking starts at the current scan position
		public void ResetPeek () { pt = tokens; }

	}
}