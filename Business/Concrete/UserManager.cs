using Business.Abstract;
using Business.Consts;
using Core.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTOs;
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

        public IResult<User> Update(UserUpdateDto userUpdate)
        {
            byte[] passwordHash, passwordSalt;
            if (userUpdate.NewPassword != "")
            {
                HashingHelper.CreatePasswordHash(userUpdate.NewPassword, out passwordHash, out passwordSalt);
            }
            else
            {
                HashingHelper.CreatePasswordHash(userUpdate.CurrentPassword, out passwordHash, out passwordSalt);
            }
            var user = new User
            {
                Id = userUpdate.Id,
                Email = userUpdate.Email,
                Name = userUpdate.Name,
                Surname = userUpdate.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userDal.Update(user);
            return new SuccessResult<User>(Messages.userUpdated);
        }

        public IResult<User> GetByMail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);

            if (user == null)
            {
                return new FailResult<User>(Messages.userNotExists);
            }

            return new SuccessResult<User>(Messages.userFetchedByMail, user);
        }

        public IResult<List<OperationClaim>> SetClaims(UserOperationClaim userOperationClaims)
        {
            _userDal.SetClaims(userOperationClaims);
            return new SuccessResult<List<OperationClaim>>(Messages.claimsSetted);
        }
    }
}
