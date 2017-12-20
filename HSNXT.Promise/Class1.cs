using System;
using System.Threading.Tasks;

namespace HSNXT.Promise
{
    public class Promise<T>
    {
        public static Task<T> New(Action<Action<T>, Action<Exception>> executor)
        {
            var completionSource = new TaskCompletionSource<T>();
            executor(result =>
            {
                completionSource.SetResult(result);
            }, exception =>
            {
                completionSource.SetException(exception);
            });
            return completionSource.Task;
        }
        public static Task<T> New(Action<Action<T>, Action> executor)
        {
            var completionSource = new TaskCompletionSource<T>();
            executor(result =>
            {
                completionSource.SetResult(result);
            }, () =>
            {
                completionSource.SetCanceled();
            });
            return completionSource.Task;
        }
        public static Task<T> New(Func<Action<T>, Action<Exception>, Task> executor)
        {
            var completionSource = new TaskCompletionSource<T>();
            executor(result =>
            {
                completionSource.SetResult(result);
            }, exception =>
            {
                completionSource.SetException(exception);
            });
            return completionSource.Task;
        }
        public static Task<T> New(Func<Action<T>, Action, Task> executor)
        {
            var completionSource = new TaskCompletionSource<T>();
            executor(result =>
            {
                completionSource.SetResult(result);
            }, () =>
            {
                completionSource.SetCanceled();
            });
            return completionSource.Task;
        }
        
        public static Task New(Action<Action, Action<Exception>> executor)
        {
            var completionSource = new TaskCompletionSource<bool>();
            executor(() =>
            {
                completionSource.SetResult(true);
            }, exception =>
            {
                completionSource.SetException(exception);
            });
            return completionSource.Task;
        }
        public static Task New(Action<Action, Action> executor)
        {
            var completionSource = new TaskCompletionSource<bool>();
            executor(() =>
            {
                completionSource.SetResult(true);
            }, () =>
            {
                completionSource.SetCanceled();
            });
            return completionSource.Task;
        }
        public static Task New(Func<Action, Action<Exception>, Task> executor)
        {
            var completionSource = new TaskCompletionSource<bool>();
            executor(() =>
            {
                completionSource.SetResult(true);
            }, exception =>
            {
                completionSource.SetException(exception);
            });
            return completionSource.Task;
        }
        public static Task New(Func<Action, Action, Task> executor)
        {
            var completionSource = new TaskCompletionSource<bool>();
            executor(() =>
            {
                completionSource.SetResult(true);
            }, () =>
            {
                completionSource.SetCanceled();
            });
            return completionSource.Task;
        }
    }
}