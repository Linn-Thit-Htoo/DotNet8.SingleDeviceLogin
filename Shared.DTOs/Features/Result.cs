using Newtonsoft.Json;
using Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTOs.Features
{
    public class Result<T>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
        public string Message { get; set; }
        public EnumStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsError => !IsSuccess;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        public static Result<T> SuccessResult(string message = "Success.", EnumStatusCode statusCode = EnumStatusCode.Success)
        {
            return new Result<T> { Message = message, StatusCode = statusCode, IsSuccess = true };
        }

        public static Result<T> SuccessResult(
            string token,
            T data,
            string message = "Success.",
            EnumStatusCode statusCode = EnumStatusCode.Success
        )
        {
            return new Result<T>
            {
                Message = message,
                Token = token,
                Data = data,
                IsSuccess = true,
                StatusCode = statusCode
            };
        }

        public static Result<T> SuccessResult(T data, string message = "Success.", EnumStatusCode statusCode = EnumStatusCode.Success)
        {
            return new Result<T> { Data = data, Message = message, StatusCode = statusCode, IsSuccess = true };
        }

        public static Result<T> SaveSuccessResult(string message = "Saving Successful.")
        {
            return Result<T>.SuccessResult(message: message);
        }

        public static Result<T> UpdateSuccessResult(string message = "Updating Successful.")
        {
            return Result<T>.SuccessResult(message: message);
        }

        public static Result<T> DeleteSuccessResult(string message = "Deleting Successful.")
        {
            return Result<T>.SuccessResult(message: message);
        }

        public static Result<T> FailureResult(string message = "Fail.", EnumStatusCode statusCode = EnumStatusCode.BadRequest)
        {
            return new Result<T> { Message = message, StatusCode = statusCode, IsSuccess = false };
        }

        public static Result<T> FailureResult(Exception ex)
        {
            return new Result<T> { Message = ex.ToString(), StatusCode = EnumStatusCode.InternalServerError, IsSuccess = false };
        }

        public static Result<T> NotFoundResult(string message = "No Data Found.")
        {
            return Result<T>.FailureResult(message, EnumStatusCode.NotFound);
        }

        public static Result<T> DuplicateResult(string message = "Duplicate Data.")
        {
            return Result<T>.FailureResult(message, EnumStatusCode.Conflict);
        }
    }
}
