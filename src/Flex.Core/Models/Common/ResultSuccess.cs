using System.Text.Json.Serialization;

namespace Flex.Core.Models.Common
{
    public class ResultSuccess<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public ResultSuccess(bool isSuccess, string message, T? data = default)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
    }
}
