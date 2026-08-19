namespace Application.Dtos;

public class Result<T>
{
    public bool IsSuccess { get; private init; }
    public ReturnType ReturnType { get; private init; }
    public string? Error { get; private init; }
    public T? Data { get; private init; }

    public static Result<T> Success(T data)
    {
        return new Result<T>()
        {
            IsSuccess = true,
            ReturnType = ReturnType.Success,
            Data = data
        };
    }

    public static Result<T> Fail(string message)
    {
        return new Result<T>()
        {
            IsSuccess = false,
            ReturnType = ReturnType.Invalid,
            Error = message
        };
    }
    public static Result<T> NotFound(string message)
    {
        return new Result<T>()
        {
            IsSuccess = false,
            ReturnType = ReturnType.NotFound,
            Error = message
        };
    }
    
    
}

public enum ReturnType
{
    Success,
    NotFound,
    Invalid
}