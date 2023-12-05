using DotJwt.Interface;
using DotJwt.Model;
using DotJwt.Service.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotJwt.Controllers {

    [Route("~/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, UserAuth {

        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserModel? userModel)
        {
            try
            {
                if(userModel == null) return BadRequest(new { message = "Please, send your information" });

                var getUserInformation = _configuration.GetSection("User").Get<UserModel>();

                if(getUserInformation == null)
                {
                    return NotFound(new { message = "User not found"});
                }

                if(getUserInformation.Email != userModel.Email || getUserInformation.Password != userModel.Password )
                {
                    return Unauthorized(new { message = "Email or password or name is wrong, please try again" });
                }


                JwtService jwtService = new JwtService();
                var token = await jwtService.GenerateToken(_configuration, userModel);

                return Ok(new { accessToken = token });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "Server exception" });
            }
        }
    }
}
