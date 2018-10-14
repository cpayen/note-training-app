using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note.Core.Models.DTO.AppUser;
using Note.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        protected readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetAsync()
        {
            var items = await _userService.GetAllAsync();
            return Ok(items.ToList());
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDTO>> GetAsync(string id)
        {
            var item = await _userService.GetAsync(id);
            return Ok(item);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<AppUserDTO>> PostAsync([FromBody] CreateAppUserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid parameter", nameof(dto));
            }

            var item = await _userService.CreateAsync(dto);
            return Ok(item);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AppUserDTO>> PutAsync(string id, [FromBody] UpdateAppUserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Invalid parameter", nameof(dto));
            }

            var item = await _userService.UpdateAsync(id, dto);
            return Ok(item);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result = await _userService.DeleteAsync(id);
            return Ok();
        }
    }
}
