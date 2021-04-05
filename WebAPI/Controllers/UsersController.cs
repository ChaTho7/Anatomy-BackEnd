using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getUser")]
        public ActionResult GetUserByMail(string email)
        {

            var result = _userService.GetByMail(email);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("updateUser")]
        public ActionResult UpdateUser(UserUpdateDto userUpdate)
        {

            var result = _userService.Update(userUpdate);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("setUserClaims")]
        public ActionResult SetUserClaims(UserOperationClaim userOperationClaims)
        {

            var result = _userService.SetClaims(userOperationClaims);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
