using Contracts.InfrastructureLayer;

namespace MVCPresentationLayer.Middleware
{
    public class AuthRedirectMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;


        public AuthRedirectMiddleware(RequestDelegate next, IJwtService jwtService)
        {
            _next = next;
            _jwtService = jwtService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Cookies.ContainsKey("jwt") && !context.Request.Path.StartsWithSegments("/Account"))
            {
                context.Response.Redirect("/Account/Login");
                return;
            }
            await _next(context);
        }
    }
}
