using Microsoft.AspNetCore.Http;

namespace Note.Core.Services
{
    // TODO: Move it to an "infrastructure" layer?
    public class CurrentWebUserService : ICurrentUserService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentWebUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetName()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
