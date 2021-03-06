﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note.Api.Filters;
using Note.Core.DTO.Note;
using Note.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class CategoriesController : ControllerBase
    {
        protected readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteCategoryDTO>>> GetAsync()
        {
            var items = await _categoryService.GetAllAsync();
            return Ok(items.ToList());
        }

        // GET api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteCategoryDTO>> GetAsync(string id)
        {
            var item = await _categoryService.GetAsync(id);
            return Ok(item);
        }

        // POST api/categories
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<NoteCategoryDTO>> PostAsync([FromBody] CreateNoteCategoryDTO dto)
        {
            var item = await _categoryService.CreateAsync(dto);
            return Ok(item);
        }

        // PUT api/categories/5
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<ActionResult<NoteCategoryDTO>> PutAsync(string id, [FromBody] UpdateNoteCategoryDTO dto)
        {
            var item = await _categoryService.UpdateAsync(id, dto);
            return Ok(item);
        }

        // DELETE api/categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return Ok();
        }
    }
}
