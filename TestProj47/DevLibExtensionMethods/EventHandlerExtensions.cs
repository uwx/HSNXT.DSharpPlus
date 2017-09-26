// Decompiled with JetBrains decompiler
// Type: DevLib.ExtensionMethods.EventHandlerExtensions
// Assembly: DevLib.ExtensionMethods, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\DevLib.ExtensionMethods.dll

using System;
using System.Threading;

namespace DevLib.ExtensionMethods
{
  /// <summary>EventHandler Extensions.</summary>
  public static class EventHandlerExtensions
  {
    /// <summary>Thread safety raise event.</summary>
    /// <param name="source">Source EventHandler.</param>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">A System.EventArgs that contains the event data.</param>
    public static void RaiseEvent(this EventHandler source, object sender, EventArgs e = null)
    {
      EventHandler eventHandler = Interlocked.CompareExchange<EventHandler>(ref source, (EventHandler) null, (EventHandler) null);
      if (eventHandler == null)
        return;
      eventHandler(sender, e);
    }

    /// <summary>Thread safety raise event.</summary>
    /// <typeparam name="T">The type of the event.</typeparam>
    /// <param name="source">Source EventHandler{T}.</param>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">A System.EventArgs that contains the event data.</param>
    public static void RaiseEvent<T>(this EventHandler<T> source, object sender, T e = null) where T : EventArgs
    {
      EventHandler<T> eventHandler = Interlocked.CompareExchange<EventHandler<T>>(ref source, (EventHandler<T>) null, (EventHandler<T>) null);
      if (eventHandler == null)
        return;
      eventHandler(sender, e);
    }
  }
}
