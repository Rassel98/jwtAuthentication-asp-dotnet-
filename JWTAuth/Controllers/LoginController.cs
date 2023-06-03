using JWTAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JWTAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }
        private User Authentication(User user)
        {
            User _user = null;
            if(user.Username=="admin" && user.Password=="admin")
            {
                _user = new User { Username="Md Jahidul Ahsan Rassel"};
            }
            return _user;
        }
        private String GenerateToken(User users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var cretential=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var token=new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],null,expires:DateTime.Now.AddMinutes(1),signingCredentials:cretential);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {
            IActionResult response = Unauthorized();
            var logUser=Authentication(user);
            if (logUser!=null)
            {
                var tokens=GenerateToken(user);
                response = Ok(new {message ="Successfully Login ",token=tokens});
            }
            return response;


        }
    }
}
