namespace ToDoListNTier.BLL.Results
{
    public class Result
    {
        public bool IsSuccess { get; init; }
        public string? Error { get; init; }
        public int? StatusCode { get; init; }

        public static Result Success() => new Result { IsSuccess = true };
        public static Result Failure(string error, int? statusCode = null) => new Result { IsSuccess = false, Error = error, StatusCode = statusCode };
    }

    public class Result<T> : Result
    {
        public T? Value { get; init; }

        public static Result<T> Ok(T value) => new Result<T> { IsSuccess = true, Value = value };
        public static new Result<T> Failure(string error, int? statusCode = null) => new Result<T> { IsSuccess = false, Error = error, StatusCode = statusCode };
    }
}
