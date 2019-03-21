using System;
using System.Collections.Generic;

namespace HSNXT.DSharpPlus.InteractivityNext
{
    /// <summary>
    /// A state holder for a pagination handle.
    /// </summary>
    public class PaginationState : BaseControlState
    {
        /// <summary>
        /// Pages that belong to this message
        /// </summary>
        public IReadOnlyList<Page> Pages { get; internal set; }
        
        /// <summary>
        /// Current page index in <see cref="Pages"/>
        /// </summary>
        public int CurrentIndex { get; internal set; }
        
        /// <summary>
        /// Timeout until pagination automatically ends
        /// </summary>
        public TimeSpan Timeout { get; internal set; }

        // TODO docs (can be used by outside for custom pagination as well as internally)
        public static PaginationState GetDefault(IReadOnlyList<Page> pages, TimeSpan timeout)
        {
            return new PaginationState
            {
                CurrentIndex = 0,
                Pages = pages,
                Timeout = timeout
            };
        }
    }
}