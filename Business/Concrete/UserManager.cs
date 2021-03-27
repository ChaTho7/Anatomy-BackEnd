using Business.Abstract;
using Business.Consts;
using Core.Concrete;
using Core.Utilities.Result;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessResult<List<OperationClaim>>(Messages.claimsFetched, _userDal.GetClaims(user));
        }

        public IResult<User> Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult<User>(Messages.userRegistered, user);
        }

        public IResult<User> GetByMail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);

            if (user==null)
            {
                return new FailResult<User>(Messages.userNotExists);
            }

            return new SuccessResult<User>(Messages.userFetchedByMail,user);
        }

        public IResult<List<OperationClaim>> SetClaims(UserOperationClaim userOperationClaims)
        {
            _userDal.SetClaims(userOperationClaims);
            return new SuccessResult<List<OperationClaim>>(Messages.claimsSetted);
        }
    }
}
