using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Note.Api.Filters;
using Note.Core.Entities.DTO.Login;
using Note.Core.Services;
using System;
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
        [Route("requesttoken")]
        public async Task<ActionResult<string>> RequestTokenAsync([FromBody] LoginDTO dto)
        {
            var claims = await _authService.LoginAsync(dto);
            if(claims == null)
            {
                return Unauthorized();
            }

            return Ok(BuildToken(claims));
        }

        private string BuildToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // TODO: Add token expiration date, handle refresh token.
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                //expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}