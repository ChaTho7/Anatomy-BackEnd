using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Utilities.Result
{
    public class SuccessResult<T> : ResultBase<T>
    {
        public SuccessResult(string message = null, T data = default) : base(true, message, data)
        {

        }
    }
}
