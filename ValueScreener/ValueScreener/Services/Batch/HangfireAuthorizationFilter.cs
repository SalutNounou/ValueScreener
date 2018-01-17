using Hangfire.Dashboard;
using Microsoft.AspNetCore.WebSockets.Internal;
using Constants = ValueScreener.Authorization.Constants;

namespace ValueScreener.Services.Batch
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return httpContext.User.IsInRole(Constants.AdministratorsRole);
        }
    }
}