using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolNet_JWT_Identity.Authentication;
using SchoolNet_JWT_Identity.Context;
using SchoolNet_JWT_Identity.Entities;

namespace SchoolNet_JWT_Identity.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly SchoolNetContext _context;

        public UsersController(SchoolNetContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(
            [FromBody] User model,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(model.Email.ToLower()) && u.Password.Equals(model.Password));
            if (user == null) return Forbid();

            ClaimsIdentity identity = new ClaimsIdentity(
                   new GenericIdentity(user.Id.ToString(), "user"),
                   new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Name", user.Name),
                        new Claim("Email", user.Email)
                   }
               );

            DateTime creationDate = DateTime.Now;
            DateTime expirationDate = creationDate +
                TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate
            });
            var token = handler.WriteToken(securityToken);

            return Ok(new TokenResult()
            {
                Authenticated = true,
                CreationDate = creationDate,
                ExpirationDate = expirationDate,
                AccessToken = token
            });
        }
    }
}