namespace Application.Common;

public class Result
{
    public bool IsSuccess { get; protected init; }
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result<T> Success<T>(T data) => new(true, Error.None, data);

    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Failure<T>(Error error) => new(false, error, default!);

    public static implicit operator Result(Error error)
    {
        return Failure(error);
    }

    public TOut Match<TOut>(Func<TOut> success, Func<Error, TOut> failure)
    {
        return IsSuccess ? success() : failure(Error);
    }
}

public class Result<T> : Result
{
    public T Data { get; }

    internal Result(bool isSuccess, Error error, T data) : base(isSuccess, error)
    {
        Data = data;
    }

    public static implicit operator Result<T>(T data) => Success<T>(data);

    public static implicit operator Result<T>(Error error) => Failure<T>(error);

    public TOut Match<TOut>(Func<T, TOut> onSuccess, Func<Error, TOut> onFailure)
    {
        return IsSuccess ? onSuccess(Data) : onFailure(Error);
    }
}