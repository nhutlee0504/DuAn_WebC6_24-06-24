using Microsoft.AspNetCore.Http;

namespace Blazor.Server.Services
{
    public class SessionRespone: ISessionServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionRespone(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("LoggedInUser");
        }
    }
}
