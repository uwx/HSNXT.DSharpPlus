﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Threading;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Concurrency;
using System.Reactive.Linq.ObservableImpl;

namespace System.Reactive.Threading.Tasks
{
    /// <summary>
    /// Provides a set of static methods for converting tasks to observable sequences.
    /// </summary>
    public static class TaskObservableExtensions
    {
        /// <summary>
        /// Returns an observable sequence that signals when the task completes.
        /// </summary>
        /// <param name="task">Task to convert to an observable sequence.</param>
        /// <returns>An observable sequence that produces a unit value when the task completes, or propagates the exception produced by the task.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> is <c>null</c>.</exception>
        /// <remarks>If the specified task object supports cancellation, consider using <see cref="Observable.FromAsync(Func{CancellationToken, Task})"/> instead.</remarks>
        public static IObservable<Unit> ToObservable(this Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            return ToObservableImpl(task, scheduler: null);
        }

        /// <summary>
        /// Returns an observable sequence that signals when the task completes.
        /// </summary>
        /// <param name="task">Task to convert to an observable sequence.</param>
        /// <param name="scheduler">Scheduler on which to notify observers about completion, cancellation or failure.</param>
        /// <returns>An observable sequence that produces a unit value when the task completes, or propagates the exception produced by the task.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> is <c>null</c> or <paramref name="scheduler"/> is <c>null</c>.</exception>
        /// <remarks>If the specified task object supports cancellation, consider using <see cref="Observable.FromAsync(Func{CancellationToken, Task})"/> instead.</remarks>
        public static IObservable<Unit> ToObservable(this Task task, IScheduler scheduler)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            if (scheduler == null)
                throw new ArgumentNullException(nameof(scheduler));

            return ToObservableImpl(task, scheduler);
        }

        private static IObservable<Unit> ToObservableImpl(Task task, IScheduler scheduler)
        {
            var res = default(IObservable<Unit>);

            if (task.IsCompleted)
            {
                scheduler = scheduler ?? ImmediateScheduler.Instance;

                switch (task.Status)
                {
                    case TaskStatus.RanToCompletion:
                        res = new Return<Unit>(Unit.Default, scheduler);
                        break;
                    case TaskStatus.Faulted:
                        res = new Throw<Unit>(task.Exception.InnerException, scheduler);
                        break;
                    case TaskStatus.Canceled:
                        res = new Throw<Unit>(new TaskCanceledException(task), scheduler);
                        break;
                }
            }
            else
            {
                //
                // Separate method to avoid closure in synchronous completion case.
                //
                res = ToObservableSlow(task, scheduler);
            }

            return res;
        }

        private static IObservable<Unit> ToObservableSlow(Task task, IScheduler scheduler)
        {
            var subject = new AsyncSubject<Unit>();

            var options = GetTaskContinuationOptions(scheduler);

            task.ContinueWith(t => ToObservableDone(task, subject), options);

            return ToObservableResult(subject, scheduler);
        }

        private static void ToObservableDone(Task task, IObserver<Unit> subject)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    subject.OnNext(Unit.Default);
                    subject.OnCompleted();
                    break;
                case TaskStatus.Faulted:
                    subject.OnError(task.Exception.InnerException);
                    break;
                case TaskStatus.Canceled:
                    subject.OnError(new TaskCanceledException(task));
                    break;
            }
        }

        /// <summary>
        /// Returns an observable sequence that propagates the result of the task.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the task.</typeparam>
        /// <param name="task">Task to convert to an observable sequence.</param>
        /// <returns>An observable sequence that produces the task's result, or propagates the exception produced by the task.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> is <c>null</c>.</exception>
        /// <remarks>If the specified task object supports cancellation, consider using <see cref="Observable.FromAsync{TResult}(Func{CancellationToken, Task{TResult}})"/> instead.</remarks>
        public static IObservable<TResult> ToObservable<TResult>(this Task<TResult> task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            return ToObservableImpl(task, scheduler: null);
        }

        /// <summary>
        /// Returns an observable sequence that propagates the result of the task.
        /// </summary>
        /// <typeparam name="TResult">The type of the result produced by the task.</typeparam>
        /// <param name="task">Task to convert to an observable sequence.</param>
        /// <param name="scheduler">Scheduler on which to notify observers about completion, cancellation or failure.</param>
        /// <returns>An observable sequence that produces the task's result, or propagates the exception produced by the task.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="task"/> is <c>null</c> or <paramref name="scheduler"/> is <c>null</c>.</exception>
        /// <remarks>If the specified task object supports cancellation, consider using <see cref="Observable.FromAsync{TResult}(Func{CancellationToken, Task{TResult}})"/> instead.</remarks>
        public static IObservable<TResult> ToObservable<TResult>(this Task<TResult> task, IScheduler scheduler)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            if (scheduler == null)
                throw new ArgumentNullException(nameof(scheduler));

            return ToObservableImpl(task, scheduler);
        }

        private static IObservable<TResult> ToObservableImpl<TResult>(Task<TResult> task, IScheduler scheduler)
        {
            var res = default(IObservable<TResult>);

            if (task.IsCompleted)
            {
                scheduler = scheduler ?? ImmediateScheduler.Instance;

                switch (task.Status)
                {
                    case TaskStatus.RanToCompletion:
                        res = new Return<TResult>(task.Result, scheduler);
                        break;
                    case TaskStatus.Faulted:
                        res = new Throw<TResult>(task.Exception.InnerException, scheduler);
                        break;
                    case TaskStatus.Canceled:
                        res = new Throw<TResult>(new TaskCanceledException(task), scheduler);
                        break;
                }
            }
            else
            {
                //
                // Separate method to avoid closure in synchronous completion case.
                //
                res = ToObservableSlow(task, scheduler);
            }

            return res;
        }

        private static IObservable<TResult> ToObservableSlow<TResult>(Task<TResult> task, IScheduler scheduler)
        {
            var subject = new AsyncSubject<TResult>();

            var options = GetTaskContinuationOptions(scheduler);

            task.ContinueWith(t => ToObservableDone(task, subject), options);

            return ToObservableResult(subject, scheduler);
        }

        private static void ToObservableDone<TResult>(Task<TResult> task, IObserver<TResult> subject)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    subject.OnNext(task.Result);
                    subject.OnCompleted();
                    break;
                case TaskStatus.Faulted:
                    subject.OnError(task.Exception.InnerException);
                    break;
                case TaskStatus.Canceled:
                    subject.OnError(new TaskCanceledException(task));
                    break;
            }
        }

        private static TaskContinuationOptions GetTaskContinuationOptions(IScheduler scheduler)
        {
            var options = TaskContinuationOptions.None;

            if (scheduler != null)
            {
                //
                // We explicitly don't special-case the immediate scheduler here. If the user asks for a
                // synchronous completion, we'll try our best. However, there's no guarantee due to the
                // internal stack probing in the TPL, which may cause asynchronous completion on a thread
                // pool thread in order to avoid stack overflows. Therefore we can only attempt to be more
                // efficient in the case where the user specified a scheduler, hence we know that the
                // continuation will trigger a scheduling operation. In case of the immediate scheduler,
                // it really becomes "immediate scheduling" wherever the TPL decided to run the continuation,
                // i.e. not necessarily where the task was completed from.
                //
                options |= TaskContinuationOptions.ExecuteSynchronously;
            }

            return options;
        }

        private static IObservable<TResult> ToObservableResult<TResult>(AsyncSubject<TResult> subject, IScheduler scheduler)
        {
            if (scheduler != null)
            {
                return subject.ObserveOn(scheduler);
            }
            else
            {
                return subject.AsObservable();
            }
        }

        /// <summary>
        /// Returns a task that will receive the last value or the exception produced by the observable sequence.
        /// </summary>
        /// <typeparam name="TResult">The type of the elements in the source sequence.</typeparam>
        /// <param name="observable">Observable sequence to convert to a task.</param>
        /// <returns>A task that will receive the last element or the exception produced by the observable sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="observable"/> is <c>null</c>.</exception>
        public static Task<TResult> ToTask<TResult>(this IObservable<TResult> observable)
        {
            if (observable == null)
                throw new ArgumentNullException(nameof(observable));

            return observable.ToTask(new CancellationToken(), state: null);
        }

        /// <summary>
        /// Returns a task that will receive the last value or the exception produced by the observable sequence.
        /// </summary>
        /// <typeparam name="TResult">The type of the elements in the source sequence.</typeparam>
        /// <param name="observable">Observable sequence to convert to a task.</param>
        /// <param name="state">The state to use as the underlying task's AsyncState.</param>
        /// <returns>A task that will receive the last element or the exception produced by the observable sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="observable"/> is <c>null</c>.</exception>
        public static Task<TResult> ToTask<TResult>(this IObservable<TResult> observable, object state)
        {
            if (observable == null)
                throw new ArgumentNullException(nameof(observable));

            return observable.ToTask(new CancellationToken(), state);
        }

        /// <summary>
        /// Returns a task that will receive the last value or the exception produced by the observable sequence.
        /// </summary>
        /// <typeparam name="TResult">The type of the elements in the source sequence.</typeparam>
        /// <param name="observable">Observable sequence to convert to a task.</param>
        /// <param name="cancellationToken">Cancellation token that can be used to cancel the task, causing unsubscription from the observable sequence.</param>
        /// <returns>A task that will receive the last element or the exception produced by the observable sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="observable"/> is <c>null</c>.</exception>
        public static Task<TResult> ToTask<TResult>(this IObservable<TResult> observable, CancellationToken cancellationToken)
        {
            if (observable == null)
                throw new ArgumentNullException(nameof(observable));

            return observable.ToTask(cancellationToken, state: null);
        }

        private sealed class ToTaskObserver<TResult> : IObserver<TResult>
        {
            private readonly CancellationToken _ct;
            private readonly IDisposable _disposable;
            private readonly TaskCompletionSource<TResult> _tcs;
            private readonly CancellationTokenRegistration _ctr = default(CancellationTokenRegistration);

            private bool _hasValue;
            private TResult _lastValue;

            public ToTaskObserver(TaskCompletionSource<TResult> tcs, IDisposable disposable, CancellationToken ct)
            {
                _ct = ct;
                _tcs = tcs;
                _disposable = disposable;

                if (ct.CanBeCanceled)
                {
                    _ctr = ct.Register(Cancel);
                }
            }

            public void OnNext(TResult value)
            {
                _hasValue = true;
                _lastValue = value;
            }

            public void OnError(Exception error)
            {
                _tcs.TrySetException(error);

                _ctr.Dispose(); // no null-check needed (struct)
                _disposable.Dispose();
            }

            public void OnCompleted()
            {
                if (_hasValue)
                {
                    _tcs.TrySetResult(_lastValue);
                }
                else
                {
                    _tcs.TrySetException(new InvalidOperationException(Strings_Linq.NO_ELEMENTS));
                }

                _ctr.Dispose(); // no null-check needed (struct)
                _disposable.Dispose();
            }

            private void Cancel()
            {
                _disposable.Dispose();
#if HAS_TPL46
                _tcs.TrySetCanceled(_ct);
#else
                _tcs.TrySetCanceled();
#endif
            }
        }

        /// <summary>
        /// Returns a task that will receive the last value or the exception produced by the observable sequence.
        /// </summary>
        /// <typeparam name="TResult">The type of the elements in the source sequence.</typeparam>
        /// <param name="observable">Observable sequence to convert to a task.</param>
        /// <param name="cancellationToken">Cancellation token that can be used to cancel the task, causing unsubscription from the observable sequence.</param>
        /// <param name="state">The state to use as the underlying task's <see cref="Task.AsyncState"/>.</param>
        /// <returns>A task that will receive the last element or the exception produced by the observable sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="observable"/> is <c>null</c>.</exception>
        public static Task<TResult> ToTask<TResult>(this IObservable<TResult> observable, CancellationToken cancellationToken, object state)
        {
            if (observable == null)
                throw new ArgumentNullException(nameof(observable));

            var tcs = new TaskCompletionSource<TResult>(state);

            var disposable = new SingleAssignmentDisposable();

            var taskCompletionObserver = new ToTaskObserver<TResult>(tcs, disposable, cancellationToken);

            //
            // Subtle race condition: if the source completes before we reach the line below, the SingleAssigmentDisposable
            // will already have been disposed. Upon assignment, the disposable resource being set will be disposed on the
            // spot, which may throw an exception.
            //
            try
            {
                //
                // [OK] Use of unsafe Subscribe: we're catching the exception here to set the TaskCompletionSource.
                //
                // Notice we could use a safe subscription to route errors through OnError, but we still need the
                // exception handling logic here for the reason explained above. We cannot afford to throw here
                // and as a result never set the TaskCompletionSource, so we tunnel everything through here.
                //
                disposable.Disposable = observable.Subscribe/*Unsafe*/(taskCompletionObserver);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }
    }
}
