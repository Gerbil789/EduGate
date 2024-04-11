namespace WebApp
{
  public class FirstPageMiddleware
  {
    private readonly RequestDelegate _next;

    public FirstPageMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context, DataService dataService)
    {
      if (context.Request.Path == "/")
      {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync($"<h1>HELLO BRUH!</h1><p>{dataService.Msg}</p>");
      }
      else
      {
        await _next(context);
      }
    }
  }
}
