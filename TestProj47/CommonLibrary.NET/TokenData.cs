namespace HSNXT.ComLib.Lang.Core
{
    /// <summary>
    /// Wraps a token with contextual information about it's script location.
    /// </summary>
    public class TokenData
    {
        /// <summary>
        /// Index position of the token in the script.
        /// </summary>
        protected int _index;


        /// <summary>
        /// The token
        /// </summary>
        public Token Token;


        /// <summary>
        /// Line number of the token
        /// </summary>
        public int Line { get; set; }


        /// <summary>
        /// Char position in the line of the token.
        /// </summary>
        public int LineCharPos { get; set; }


        /// <summary>
        /// The position of the first char of token based on entire script.
        /// </summary>
        public int Pos { get; set; }


        /// <summary>
        /// The index position of the token.
        /// </summary>
        public int Index => _index;


        /// <summary>
        /// Sets the index of this token.
        /// </summary>
        /// <param name="ndx"></param>
        internal void SetIndex(int ndx)
        {
            _index = ndx;
        }


        /// <summary>
        /// String representation of tokendata.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {   
            var tokenType = Token.GetType().Name.Replace("Token", "");
            var info =
                $"Index: {Index}, Line: {Line}, CharPos: {LineCharPos}, Pos: {Pos}, Type: {tokenType}, Text: {Token.Text}";
            return info;
        }
    }
}
