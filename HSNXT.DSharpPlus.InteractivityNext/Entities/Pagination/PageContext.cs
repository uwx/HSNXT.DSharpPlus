using System;
using System.Collections.Generic;

namespace DSharpPlus.Interactivity
{
    internal class PageContext
    {
        public IList<Page> Pages { get; set; }
        public int CurrentIndex { get; set; }
        public bool IsClosing { get; set; }
    }
}