using System.Net;

namespace CoffeeShop.Application.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }

        public static Result SuccessResponse(string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                StatusCode = (int)statusCode
            };
        }
        public static Result ErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                StatusCode = (int)statusCode
            };
        }
    }
    public class Result<T> : Result
    {
        public T? Data { get; set; }
        public static Result<T> SuccessResponse(T data, string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Message = message,
                StatusCode = (int)statusCode,
                Data = data
            };
        }

        public static new Result<T> ErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message,
                StatusCode = (int)statusCode
            };
        }
    }
}

