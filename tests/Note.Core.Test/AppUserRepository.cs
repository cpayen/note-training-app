using Note.Core.Data;
using Note.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Core.Test
{
    class AppUserRepository : IRepository<AppUser>
    {
        public Task<AppUser> CreateItemAsync(AppUser item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppUser>> GetItemsAsync()
        {
            var _appUsers = new List<AppUser>
            {
                new AppUser
                {
                    Id = "Id",
                    Name = "Name",
                    Email = "user@test.com",
                    Salt = "iZFD3Mg3n6VyQzr4GTXpqg==",
                    Password = "OrCYpUsn1AxuircI7j8Lo0UXFyfXGBzdRSZEsVEBno8=", // Pa$$w0rd
                    Role = 0
                }
            };

            return Task.FromResult<IEnumerable<AppUser>>(_appUsers);
        }

        public Task<IEnumerable<AppUser>> GetItemsAsync(System.Linq.Expressions.Expression<Func<AppUser, bool>> predicate)
        {
            var _appUsers = new List<AppUser>
            {
                new AppUser
                {
                    Id = "Id",
                    Name = "Name",
                    Email = "user@test.com",
                    Salt = "iZFD3Mg3n6VyQzr4GTXpqg==",
                    Password = "OrCYpUsn1AxuircI7j8Lo0UXFyfXGBzdRSZEsVEBno8=", // Pa$$w0rd
                    Role = 0
                }
            };

            return Task.FromResult<IEnumerable<AppUser>>(_appUsers);
        }

        public Task<AppUser> UpdateItemAsync(string id, AppUser item)
        {
            throw new NotImplementedException();
        }
    }
}
