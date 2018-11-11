using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Note.Api.Filters;
using Note.Core.DTO.Login;
using Note.Core.Services;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    // TODO: Add token expiration, token refresh...

    [Route("api/v1/auth")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        protected readonly IAuthService _authService;
        protected readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateModel]
        [Route("login")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] LoginDTO dto)
        {
            var user = await _authService.LoginAsync(dto);
            if(user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            user.Token = BuildToken(claims);

            return Ok(user);
        }

        private string BuildToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                signingCredentials: creds,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}