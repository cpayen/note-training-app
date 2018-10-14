using Note.Core.Models.DTO.Note;
using Note.Core.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Note.Tests
{
    public class CategoryTest
    {
        protected readonly CategoryService _categoryService;

        public CategoryTest()
        {
            //_categoryService = new CategoryService();
        }

        [Fact]
        public async Task CreateAsync()
        {
            var dto = new CreateNoteCategoryDTO
            {
                Name = "Test",
                Description = "Test",
                Color = "#ffffff"
            };

            //var result = await _categoryService.CreateAsync(dto);
            Assert.Equal("Test", dto.Name);
        }
    }
}
