using System;
using System.Collections.Generic;

namespace DSharpPlus.Interactivity
{
    public class PaginatedMessage
    {
        public IEnumerable<Page> Pages { get; internal set; }
        public int CurrentIndex { get; internal set; }
        public TimeSpan Timeout { get; internal set; }
    }
}