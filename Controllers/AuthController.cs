using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dot_Net_7_jumpstart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

       [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto user)
        {
            var response = await _authRepo.Register(
                new User{Username=user.UserName},user.Password
            );
               if(!response.Success)
                 return BadRequest(response);
              return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto user)
        {
           var response = await _authRepo.Login(user.UserName,user.Password);
           if(!response.Success)
             return BadRequest(response);
           return Ok(response);   
        }
    }
}