using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Car.WebApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        private readonly IConfiguration config;

        public LoginController(IConfiguration config, ILoginService loginService)
        {
            this.config = config;
            this.loginService = loginService;
        }

        /// <summary>
        /// ensures the user and returns a UserDTO for client app,
        /// if user doesn't exist in DB it creates a user and saves them to DB
        /// </summary>
        /// <param name="userModel">User model params</param>
        /// <returns>UserDTO for a client app</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserDto userModel)
        {
            var user = EnsureUser(userModel);
            var tokenString = GenerateJSONWebToken(user);

            UserDto userDTO = new UserDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Id = user.Id,
                Location = user.Location,
                Position = user.Position,
                Email = user.Email,
                Token = tokenString,
            };

            return Ok(userDTO);
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User EnsureUser(UserDto login)
        {
            var defaultUser = loginService.GetUser(login.Email);
            if (defaultUser == null)
            {
                defaultUser = new User()
                {
                    Name = login.Name,
                    Surname = login.Surname,
                    Email = login.Email,
                    Position = login.Position,
                    Location = login.Location,
                    UserPreferences = new UserPreferences()
                    {
                        DoAllowEating = false,
                        DoAllowSmoking = false,
                        Comments = string.Empty,
                    },
                };

                loginService.SaveUser(defaultUser);
            }

            return defaultUser;
        }
    }
}
