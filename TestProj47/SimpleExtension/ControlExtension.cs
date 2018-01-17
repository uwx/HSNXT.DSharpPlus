#if NetFX
// Decompiled with JetBrains decompiler
// Type: SimpleExtension.ControlExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: ...\bin\Debug\SimpleExtension.dll

using System;
using System.Windows.Forms;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static void UiThreadInvoke(this Control control, Action code)
        {
            if (control.InvokeRequired)
                control.Invoke(code);
            else
                code();
        }

        public static void UiThreadInvoke(this UserControl control, Action code)
        {
            if (control.InvokeRequired)
                control.Invoke(code);
            else
                code();
        }
    }
}
#endif