using System;
using System.Diagnostics.CodeAnalysis;

namespace Abyssal.Common
{
    public static class ExceptionHelper
    {
        public static BoxedExecutionResult<T> ExecuteBoxed<T>(Func<T> func)
        {
            try
            {
                return new BoxedExecutionResult<T>(true, null, func());
            }
            catch (Exception e)
            {
                return new BoxedExecutionResult<T>(false, e, default);
            }
        }

        public static BoxedExecutionResult ExecuteBoxed(Action action)
        {
            try
            {
                action();
                return new BoxedExecutionResult(true, null);
            }
            catch (Exception e)
            {
                return new BoxedExecutionResult(false, e);
            }
        }

        public static bool TryExecuteBoxed<T>(Func<T> func, [NotNullWhen(true)] out T? result)
        {
            var boxedResult = ExecuteBoxed(func);
            result = boxedResult.Result;
            return boxedResult.IsSuccessful;
        }

        public static bool TryExecuteBoxed(Action action)
        {
            return ExecuteBoxed(action).IsSuccessful;
        }
    }

    public class BoxedExecutionResult<T> : BoxedExecutionResult
    {
        public T? Result { get; }

        internal BoxedExecutionResult(bool isSuccessful, Exception? exception, T? result) : base(isSuccessful, exception)
        {
            Result = result;
        }
    }

    public class BoxedExecutionResult
    {
        public bool IsSuccessful { get; }
        
        [MemberNotNullWhen(false, "IsSuccessful")]
        public Exception? Exception { get; }

        internal BoxedExecutionResult(bool isSuccessful, Exception? exception)
        {
            IsSuccessful = isSuccessful;
            Exception = exception;
        }
    }
}