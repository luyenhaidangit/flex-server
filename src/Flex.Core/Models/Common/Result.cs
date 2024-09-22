using System.Text.Json.Serialization;

namespace Flex.Core.Models.Common
{
    public class Result
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; }

        public Result(bool isSuccess, string message, object? data = default)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static Result Success(string message = Shared.Constants.System.Message.Success, object? data = default)
        {
            return new Result(true, message, data);
        }

        public static Result Failure(string message = Shared.Constants.System.Message.Failure)
        {
            return new Result(false, message);
        }
    }
}
