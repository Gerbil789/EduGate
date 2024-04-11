namespace WebApp
{
  public class ErrorMiddleware
  {
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context, ErrorHandler handler)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        await handler.Handle(ex);

        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync($"<h1>ERROR: {ex.Message}</h1>");
      }
    }
  }
}
