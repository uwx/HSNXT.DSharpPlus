// <lang:using>

using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Helpers;
using HSNXT.ComLib.Lang.Parsing;
using HSNXT.ComLib.Lang.Types;

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Type of plugins gets the type of an expression.
    
    function inc(a) { return a + 1; }
    
    dataType = typeof 'fluentscript'    // 'string'
    dataType = typeof 12                // 'number'
    dataType = typeof 12.34             // 'number'
    dataType = typeof true              // 'boolean'
    dataType = typeof false             // 'boolean'
    dataType = typeof new Date()        // 'datetime'
    dataType = typeof 3pm               // 'time'
    dataType = typeof [0, 1, 2]         // 'object:list'
    dataType = typeof { name: 'john' }  // 'object:map'   
    dataType = typeof new User('john')  // 'object:ComLib.Lang.Tests.Common.User'
    dataType = typeof inc               // 'function:inc'
    
    </doc:example>
    ***************************************************************************/

    /// <summary>
    /// Combinator for handling swapping of variable values. swap a and b.
    /// </summary>
    public class TypeOfPlugin : ExprPlugin
    {
        /// <summary>
        /// Intialize.
        /// </summary>
        public TypeOfPlugin()
        {
            this.IsStatement = false;
            this.IsAutoMatched = true;
            this.StartTokens = new[] { "typeof" };
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "typeof <expression>";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "typeof 3",
            "typeof 'abcd'",
            "typeof user"
        };


        /// <summary>
        /// run step 123.
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {                        
            // The expression to round.
            _tokenIt.Advance(1, false);
            var exp = _parser.ParseExpression(null, false, true, true, false);
            var typeExp = new TypeOfExpr(exp);
            if (exp is NewExpr && _tokenIt.NextToken.Token == Tokens.RightParenthesis)
            {
                //typeExp.SupportsBoundary = true;
                //typeExp.BoundaryText = ")";
            }

            return typeExp;
        }
    }



    /// <summary>
    /// Variable expression data
    /// </summary>
    public class TypeOfExpr : Expr
    {
        private readonly Expr _exp;
        
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="exp">The expression value to round</param>
        public TypeOfExpr(Expr exp)
        {
            _exp = exp;
        }


        /// <summary>
        /// Evaluate
        /// </summary>
        /// <returns></returns>
        public override object Evaluate()
        {
            // 1. Check for function ( currently does not support functions as first class types )
            if (_exp is VariableExpr || _exp is FunctionCallExpr)
            {
                var name = _exp.ToQualifiedName();
                if (Ctx.Functions.Contains(name))
                    return new LString("function:" + name);
            }
            var obj = _exp.Evaluate();
            ExceptionHelper.NotNull(this, obj, "typeof");
            var lobj = (LObject) obj;
            var typename = lobj.Type.Name;

            if (lobj.Type == LTypes.Array || lobj.Type == LTypes.Map)
                typename = "object:" + typename;
            else if (lobj.Type == LTypes.Bool)
                typename = "boolean";
            else if (lobj.Type.TypeVal == TypeConstants.LClass)
                typename = "object:" + lobj.Type.FullName;

            return new LString(typename);
        }
    }
}
