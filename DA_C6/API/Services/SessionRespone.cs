using Microsoft.AspNetCore.Http;

namespace Blazor.Server.Services
{
    public class SessionRespone : ISessionServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionRespone(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {
            var context = _httpContextAccessor.HttpContext;
            return context?.Session.GetString("UserName");
        }
    }
}
