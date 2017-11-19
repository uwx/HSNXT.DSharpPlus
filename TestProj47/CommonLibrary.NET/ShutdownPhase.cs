﻿using HSNXT.ComLib.Lang.Helpers;
using HSNXT.ComLib.Lang.Parsing;

namespace HSNXT.ComLib.Lang.Phases
{
    /// <summary>
    /// Executes the code represented as an AST.
    /// </summary>
    public class ShutdownPhase : Phase
    {
        /// <summary>
        /// initializes this phase.
        /// </summary>
        public ShutdownPhase()
        {
            this.Name = "interpreter-shutdown";
        }


        /// <summary>
        /// Executes all the statements in the script.
        /// </summary>
        public override PhaseResult Execute(PhaseContext phaseCtx)
        {
            var result = LangHelper.Execute(() => phaseCtx.Ctx.Plugins.Dispose());
            return new PhaseResult(result);
        }
    }
}
