namespace MVCPresentationLayer.Middleware
{
    public class AuthRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Cookies.ContainsKey("jwt") && !context.Request.Path.StartsWithSegments("/Authentication"))
            {
                context.Response.Redirect("/Authentication/Login");
                return;
            }

            await _next(context);
        }
    }
}
