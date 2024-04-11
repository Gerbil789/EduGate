namespace WebApp
{
  public class FileMiddleware
  {
    private readonly RequestDelegate _next;

    public FileMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      await File.AppendAllTextAsync("log.txt", $"REQUEST: {DateTime.Now} - {context.Request.Path}\n");

      var path = context.Request.Path.ToString();
      path = path.TrimStart('/');

      string dir = @"C:\Users\vojta\Downloads";

      path = Path.Combine(dir, path);

      if (File.Exists(path))
      {
        context.Response.ContentType = "image/jpeg";
        await context.Response.SendFileAsync(path);
        return;
      }

      await _next(context);
    }
  }
}
