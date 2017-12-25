﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System.Collections.Generic;
using HSNXT.Reactive;
using System;

namespace Microsoft.Reactive.Testing
{
    class MockObserver<T> : ITestableObserver<T>
    {
        TestScheduler scheduler;
        List<Recorded<Notification<T>>> messages;

        public MockObserver(TestScheduler scheduler)
        {
            if (scheduler == null)
                throw new ArgumentNullException(nameof(scheduler));

            this.scheduler = scheduler;
            messages = new List<Recorded<Notification<T>>>();
        }

        public void OnNext(T value)
        {
            messages.Add(new Recorded<Notification<T>>(scheduler.Clock, Notification.CreateOnNext<T>(value)));
        }

        public void OnError(Exception exception)
        {
            messages.Add(new Recorded<Notification<T>>(scheduler.Clock, Notification.CreateOnError<T>(exception)));
        }

        public void OnCompleted()
        {
            messages.Add(new Recorded<Notification<T>>(scheduler.Clock, Notification.CreateOnCompleted<T>()));
        }

        public IList<Recorded<Notification<T>>> Messages
        {
            get { return messages; }
        }
    }
}
