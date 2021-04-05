using Core.Concrete;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserDal : EFEntityRepositoryBase<AnatomyDB, User>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (AnatomyDB context = new AnatomyDB())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public void SetClaims(UserOperationClaim userOperationClaims)
        {
            using (AnatomyDB context = new AnatomyDB())
            {
                var addedClaims = context.Entry(userOperationClaims);
                addedClaims.State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
