namespace CommerceCore.WebApi.Middlewares;

public class CookieTokenMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("access_token", out var token))
        {
            context.Request.Headers.Authorization = $"Bearer {token}";
        }

        await _next(context);
    }
}
