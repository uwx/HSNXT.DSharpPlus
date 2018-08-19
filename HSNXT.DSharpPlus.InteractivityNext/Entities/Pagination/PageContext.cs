using System;
using System.Collections.Generic;

namespace DSharpPlus.Interactivity
{
    public class PageContext
    {
        public IList<Page> Pages { get; internal set; }
        public int CurrentIndex { get; internal set; }
        public TimeSpan Timeout { get; internal set; }
    }
}