using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace projektowaniaOprogramowania.Services
{
    public class SessionWrapper : ISessionWrapper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionWrapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long? GetUserId()
        {
            return _httpContextAccessor.HttpContext.Session.GetLong("UserId");
        }
    }
}
