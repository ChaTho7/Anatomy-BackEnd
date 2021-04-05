using Core.Concrete;
using Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult<List<OperationClaim>> GetClaims(User user);
        IResult<List<OperationClaim>> SetClaims(UserOperationClaim userOperationClaims);
        IResult<User> Add(User user);
        IResult<User> Update(User user);
        IResult<User> GetByMail(string email);
    }
}
