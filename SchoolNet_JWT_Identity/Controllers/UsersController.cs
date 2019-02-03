using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolNet_JWT_Identity.ApplicationUserSetup;
using SchoolNet_JWT_Identity.Authentication;
using SchoolNet_JWT_Identity.Context;
using SchoolNet_JWT_Identity.Entities;

namespace SchoolNet_JWT_Identity.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly SchoolNetContext _context;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public UsersController(
            SchoolNetContext context,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations
            )
        {
            _context = context;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] User model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(
                u => u.Email.ToLower().Equals(model.Email.ToLower()) &&
                u.PasswordHash.Equals(model.PasswordHash));

            if (user == null) return Forbid();

            var tokenResult = GenerateToken(user);

            return Ok(tokenResult);
        }
        
        [HttpPost("new-user")]
        public async Task<IActionResult> CreateUserAsync(
            [FromBody] User user,
            [FromServices] UserManager<User> userManager)
        {
            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
                return Created("", user);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var error in result.Errors)
                stringBuilder.AppendLine($"{error.Code}: {error.Description}");

            return BadRequest(stringBuilder.ToString());
        }



        private TokenResult GenerateToken(User user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                 new GenericIdentity(user.Id.ToString(), "user"),
                 new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Name", user.UserName),
                        new Claim("Email", user.Email)
                 }
             );

            DateTime creationDate = DateTime.Now;
            DateTime expirationDate = creationDate +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return new TokenResult()
            {
                Authenticated = true,
                CreationDate = creationDate,
                ExpirationDate = expirationDate,
                AccessToken = token
            };
        }
    }
}