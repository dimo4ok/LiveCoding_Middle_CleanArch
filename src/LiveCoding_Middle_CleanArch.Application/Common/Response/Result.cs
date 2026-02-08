using Microsoft.AspNetCore.Http;

namespace LiveCoding_Middle_CleanArch.Application.Common.Response;

public class Result
{
    public bool IsSuccess { get; }
    public int StatusCode { get; }
    public string? Error { get; }

    private Result(bool success, int statusCode, string? error)
    {
        IsSuccess = success;
        StatusCode = statusCode;
        Error = error;
    }

    public static Result Success(int statusCode = StatusCodes.Status200OK) => new (true, statusCode, null);
    public static Result Fail(string error, int statusCode = StatusCodes.Status404NotFound) => new (false, statusCode, error);
}

public class Result<T>
{
    public bool IsSuccess { get; }
    public int StatusCode { get; }
    public T? Value { get; }
    public string? Error { get; }

    private Result(bool success, int statusCode, T? value, string? error)
    {
        IsSuccess = success;
        StatusCode = statusCode;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value, int statusCode = StatusCodes.Status200OK) => new(true, statusCode, value, null);
    public static Result<T> Fail(string error, int statusCode = StatusCodes.Status404NotFound) => new (false, statusCode, default, error);
}