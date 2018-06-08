namespace rpn_csharp.Tokens
{
    public interface IOperator : IToken
    {
        void Apply();
    }
}