using AutoMapper;
using Moq;
using Note.Core.Data;
using Note.Core.DTO.Login;
using Note.Core.Entities;
using Note.Core.Profiles;
using Note.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Note.Core.Test
{
    public class AuthServiceTest
    {
        protected readonly AuthService _service;

        public AuthServiceTest()
        {
            //var _appUsers = new List<AppUser>
            //{
            //    new AppUser
            //    {
            //        Id = "Id",
            //        Name = "Name",
            //        Email = "user@test.com",
            //        Salt = "iZFD3Mg3n6VyQzr4GTXpqg==",
            //        Password = "OrCYpUsn1AxuircI7j8Lo0UXFyfXGBzdRSZEsVEBno8=", // Pa$$w0rd
            //        Role = 0
            //    }
            //};

            // Mock repository
            //var repo = new Mock<IRepository<AppUser>>();
            //repo
            //    .Setup(o => o.GetItemsAsync(It.IsAny<Expression<Func<AppUser, bool>>>()))
            //    .Returns(Task.FromResult<IEnumerable<AppUser>>(_appUsers));
            var repo = new AppUserRepository();

            // Automapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppUserProfile());
            });
            var mapper = config.CreateMapper();
            
            // Initialize service
            _service = new AuthService(repo, mapper);
        }

        [Fact]
        public async Task LoginErrorAsync()
        {
            LoginDTO dto;
            AuthenticatedUserDTO result;

            dto = new LoginDTO
            {
                Email = "fake",
                Password = "fake"
            };
            result = await _service.LoginAsync(dto);
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginSuccessAsync()
        {
            LoginDTO dto;
            AuthenticatedUserDTO result;

            dto = new LoginDTO
            {
                Email = "user@test.com",
                Password = "Pa$$w0rd"
            };
            result = await _service.LoginAsync(dto);
            Assert.NotNull(result);
            Assert.IsType<AuthenticatedUserDTO>(result);
        }
    }
}
