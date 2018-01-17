// Decompiled with JetBrains decompiler
// Type: TestProj47.EventHandlerExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Threading;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>Thread safety raise event.</summary>
        /// <param name="source">Source EventHandler.</param>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A System.EventArgs that contains the event data.</param>
        public static void RaiseEvent(this EventHandler source, object sender, EventArgs e = null)
        {
            var eventHandler = Interlocked.CompareExchange(ref source, null, null);
            if (eventHandler == null)
                return;
            eventHandler(sender, e);
        }
    }
}