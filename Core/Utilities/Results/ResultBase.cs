using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class ResultBase<T> : IResult<T>
    {
        public T Data { get; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ResultBase(bool success, string message, T data = default)
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }
}
