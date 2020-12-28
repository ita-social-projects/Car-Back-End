using CarBackEnd.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Car.DAL.Entities;
using Car.BLL.Services.Interfaces;

namespace CarBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config, ILoginService loginService)
        {
            _config = config;
            _loginService = loginService;
        }

        /// <summary>
        /// ensures the user and returns a token for client app,
        /// if user doesn't exist in DB it creates a user and saves them to DB
        /// </summary>
        /// <param name="userModel">User model params</param>
        /// <returns>token for a client app and user Id</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel userModel)
        {
            var user = EnsureUser(userModel);

            var tokenString = GenerateJSONWebToken(user);

            return Ok(new { token = tokenString, userId = user.Id });
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User EnsureUser(UserModel login)
        {
            var defaultUser = _loginService.GetUser(login.EmailAddress);
            if (defaultUser == null)
            {
                defaultUser = new User()
                {
                    Name = login.Name,
                    Surname = login.Surname,
                    Email = login.EmailAddress,
                };

                _loginService.SaveUser(defaultUser);
            }

            return defaultUser;
        }
    }
}
