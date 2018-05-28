namespace DCLite
{
    public interface IErrors
    {
        int Count { get; }

        void SynErr (int line, int col, int n);
        void SemErr (int line, int col, string s);
        void SemErr (string s);
        void Warning (int line, int col, string s);
        void Warning(string s);
    }
}