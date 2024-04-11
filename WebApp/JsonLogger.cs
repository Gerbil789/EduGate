using System.Text.Json;

namespace WebApp
{
  public class JsonLogger : IMyLogger
  {
    public async Task Log(string message)
    {
      await File.AppendAllTextAsync("log.json", JsonSerializer.Serialize(new { Time = DateTime.Now, Message = message }) + "\n");
    }
  }
}
