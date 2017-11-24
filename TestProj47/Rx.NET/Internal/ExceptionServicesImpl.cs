// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System.Runtime.ExceptionServices;
using System;

namespace HSNXT.Reactive.PlatformServices
{
    //
    // WARNING: This code is kept *identically* in two places. One copy is kept in HSNXT.Reactive.Core for non-PLIB platforms.
    //          Another copy is kept in HSNXT.Reactive.PlatformServices to enlighten the default lowest common denominator
    //          behavior of Rx for PLIB when used on a more capable platform.
    //
    internal class /*Default*/ExceptionServicesImpl : IExceptionServices
    {
        public void Rethrow(Exception exception) => ExceptionDispatchInfo.Capture(exception).Throw();
    }
}