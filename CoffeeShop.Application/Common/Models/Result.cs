namespace CoffeeShop.Application.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public static Result SuccessResponse(string? message = null)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message
            };
        }
        public static Result ErrorResponse(string message)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
    public class Result<T> : Result
    {
        public T? Data { get; set; }
        public static Result<T> SuccessResponse(T data, string? message = null)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }
        public static Result<T> ErrorResponse(string message)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}

