using Microsoft.AspNetCore.Http;
using Note.Core.Services;

namespace Note.Infra.User
{
    public class CurrentWebUserInfo : ICurrentUserInfo
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentWebUserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetName()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
