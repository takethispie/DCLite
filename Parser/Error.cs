using System;

namespace DCLite
{
    public class Errors : IErrors {
        private int count = 0;
        public int Count => count;
        public System.IO.TextWriter errorStream = Console.Out;
        public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

        public virtual void SynErr (int line, int col, int n) {
            string s;
            switch (n) {
                case 0: s = "EOF expected"; break;
                case 1: s = "ident expected"; break;
                case 2: s = "number expected"; break;
                case 3: s = "\"program\" expected"; break;
                case 4: s = "\"{\" expected"; break;
                case 5: s = "\"store\" expected"; break;
                case 6: s = "\"}\" expected"; break;
                case 7: s = "\"add\" expected"; break;
                case 8: s = "\"sub\" expected"; break;
                case 9: s = "\"mul\" expected"; break;
                case 10: s = "\"div\" expected"; break;
                case 11: s = "??? expected"; break;
                case 12: s = "invalid Arithm"; break;
                case 13: s = "invalid Arithm"; break;
                case 14: s = "invalid Arithm"; break;

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
    }
}
