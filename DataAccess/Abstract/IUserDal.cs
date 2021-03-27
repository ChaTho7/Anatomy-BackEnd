using System.Collections.Generic;
using Core.Abstract;
using Core.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        void SetClaims(UserOperationClaim userOperationClaims);
    }
}
