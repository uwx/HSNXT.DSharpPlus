using System;
using System.Collections.Generic;
using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>
    // Holiday plugin allows references to dates using Holiday names such as:
    // Christmas
    // Independence day
    // Valentines day
    // New Years
    
    if today is New Years 2012 then 
	    print happy new year!
    </doc:example>
    ***************************************************************************/

    /// <summary>
    /// Combinator for handling holidays.
    /// TODO: next holiday, previous holiday.
    /// </summary>
    public class HolidayPlugin : ExprPlugin
    {
        private readonly Dictionary<string, Func<DateTime>> _holidays;


        private readonly int _maxWordsInHoliday = 3;


        /// <summary>
        /// Initialize
        /// </summary>
        public HolidayPlugin()
        {
            _holidays = new Dictionary<string,Func<DateTime>>();
            _holidays["New Years"       ] = () => Convert.ToDateTime("1/1/"   + DateTime.Now.Year);
            _holidays["Valentines Day"  ] = () => Convert.ToDateTime("2/14/"  + DateTime.Now.Year);
            _holidays["Independence Day"] = () => Convert.ToDateTime("7/4/"   + DateTime.Now.Year);
            _holidays["Christmas Eve"   ] = () => Convert.ToDateTime("12/24/" + DateTime.Now.Year);
            _holidays["Christmas"       ] = () => Convert.ToDateTime("12/25/" + DateTime.Now.Year);
            _holidays["New Years Eve"   ] = () => Convert.ToDateTime("12/31/" + DateTime.Now.Year);

            _holidays["new years"] = () => Convert.ToDateTime("1/1/" + DateTime.Now.Year);
            _holidays["valentines day"] = () => Convert.ToDateTime("2/14/" + DateTime.Now.Year);
            _holidays["independence day"] = () => Convert.ToDateTime("7/4/"   + DateTime.Now.Year);
            _holidays["christmas eve"   ] = () => Convert.ToDateTime("12/24/" + DateTime.Now.Year);
            _holidays["christmas"       ] = () => Convert.ToDateTime("12/25/" + DateTime.Now.Year);
            _holidays["new years eve"] = () => Convert.ToDateTime("12/31/" + DateTime.Now.Year);
            this.StartTokens = new[] 
            {
                "New", "Valentines", "Independence", "Christmas",
                "new", "valentines", "christmas", "independence"
            };
            this.Precedence = 100;
        }


        /// <summary>
        /// Whether or not this parser can handle the supplied token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public override bool CanHandle(Token token)
        {
            var name = token.Text.ToLower();
            var name2 = _tokenIt.Peek().Token.Text.ToLower();
            if (_holidays.ContainsKey(name)) return true;
            if (_holidays.ContainsKey(name + " " + name2)) return true;
            return false;
        }


        /// <summary>
        /// Parses the holidays into dates.
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {
            var date = DateTime.MinValue;
            string holidayName = null;
            TokenData next = null;

            // Multi-word holiday.
            holidayName = _tokenIt.NextToken.Token.Text.ToLower();
            var count = 0;
            var matched = false;

            // Can only peek() at most the maximum number of words in a holiday.
            while (count < _maxWordsInHoliday)
            {
                var peek1More = _tokenIt.Peek(count + 1);
                var holidayPlus1 = holidayName + " " + peek1More.Token.Text.ToLower();
                var isNextWordApplicable = _holidays.ContainsKey(holidayPlus1);
                    
                if (_holidays.ContainsKey(holidayName) && !isNextWordApplicable)
                {
                    // Peek one more time.
                    date = _holidays[holidayName]();
                    matched = true;
                    break;
                }
                count++;
                next = _tokenIt.Peek(count);   
                holidayName += " " + next.Token.Text.ToLower();                
            }
            if (matched && count > 0)
                _tokenIt.Advance(count);

            // Is there a year specified afterwards?
            // e.g. new years eve 2011 ?
            next = _tokenIt.Peek();
            if (next.Token.Type == TokenTypes.LiteralNumber)
            {
                var year = Convert.ToInt32(next.Token.Text);
                date = new DateTime(year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
                _tokenIt.Advance();
            }
            // Finally move past this plugin.
            _tokenIt.Advance();
            return new ConstantExpr(date);
        }
    }
}
