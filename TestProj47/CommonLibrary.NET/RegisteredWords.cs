using System.Collections.Generic;

// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Parsing
{
    /// <summary>
    /// Helper class for calling functions
    /// </summary>
    public class RegisteredWords : Dictionary<string, string>
    {
        /// <summary>
        /// Register a custom type into the interpreter( for calling c# from the language ).
        /// </summary>
        /// <param name="word">The word to register</param>
        public void Register(string word)
        {
            this[word] = word;
        }
    }
}
