namespace CulturalEvents.App.Core;

public class Result<T>
{
    public T? Value { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Exception? Exception { get; }
    private Result(bool isSuccess = true)
    {
        IsSuccess = isSuccess;
        Value = default;
    }
    private Result(T? value, bool isSuccess = true)
    {
        Value = value;
        IsSuccess = isSuccess;
    }

    private Result(Exception exception)
    {
        Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        IsSuccess = false;
    }
    public static Result<T> Success() => new Result<T>(true);
    public static Result<T> Success(T value) => new Result<T>(value);
    public static Result<T> Failure() => new Result<T>(false);
    public static Result<T> Failure(T value) => new Result<T>(value, false);
    public static Result<T> Failure(Exception exception) => new Result<T>(exception);
    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Exception exception) => Failure(exception);

    public static implicit operator T(Result<T> result)
    {
        if (result is { IsFailure: true, Value: null })
            throw new InvalidOperationException("Cannot retrieve value from a failed result.", result.Exception);
        return result.Value!;
    }

    public static implicit operator Exception?(Result<T> result) => result.Exception;

    public void Match(Action<T>? onSuccess, Action<Exception>? onFailure)
    {
        if (IsSuccess && onSuccess is not null)
        {
            onSuccess(Value!);
        }
        else if (IsFailure && onFailure is not null)
        {
            onFailure(Exception!);
        }
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Exception, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Exception!);
    }
}