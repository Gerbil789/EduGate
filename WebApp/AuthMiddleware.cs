namespace WebApp
{
  public class AuthMiddleware
  {
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context, DataService dataService)
    {
      string userAgent = context.Request.Headers.UserAgent;
      dataService.Msg = userAgent;

      if(userAgent.Contains("Chrome") && !userAgent.Contains("Edg") && !userAgent.Contains("OPR"))
      {
        await _next(context);
        return;
      }
      context.Response.ContentType = "text/html; charset=utf-8";
      await context.Response.WriteAsync("<h1>USE CHROME!</h1>");
    }
  }
}
