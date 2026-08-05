namespace Shared.Results;

/// <summary>
/// Resultado de una operación
/// </summary>
public class Result
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public string[]? Errors { get; }

    protected Result(bool isSuccess, string message, string[]? errors = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors;
    }

    public static Result Success(string message = "Operación exitosa")
        => new(true, message);

    public static Result Failure(string message, string[]? errors = null)
        => new(false, message, errors);
}

/// <summary>
/// Resultado con datos
/// </summary>
public class Result<T> : Result
{
    public T? Data { get; }

    private Result(bool isSuccess, string message, T? data = default, string[]? errors = null)
        : base(isSuccess, message, errors)
    {
        Data = data;
    }

    public static Result<T> Success(T data, string message = "Operación exitosa")
        => new(true, message, data);

    public new static Result<T> Failure(string message, string[]? errors = null)
        => new(false, message, default, errors);
}