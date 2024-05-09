using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using MailKit;
using MimeKit;


namespace HttpClientApp
{
  class Program
  {

    static async Task Main(string[] args)
    {
      var url = "https://www.7timer.info/bin/astro.php?lon=18.160005506399536&lat=49.831015379859586&ac=0&unit=metric&output=json&tzshift=0";

      try
      {
        using MimeMessage msg = new();
        msg.From.Add(new MailboxAddress("Bruhman", "atnet2019@seznam.cz"));
        msg.To.Add(new MailboxAddress("Csharp", "vojta.rubes.01@gmail.com"));

        msg.Subject = "Bruh";
        BodyBuilder body = new BodyBuilder();
        body.TextBody = "text Bruh";
        body.HtmlBody = "<h1>html Bruh</h1>";

        //add image attachment
        body.Attachments.Add(@"C:\Users\vojta\Downloads\game_progress_diagram.png");


        msg.Body = body.ToMessageBody();


        using var client = new MailKit.Net.Smtp.SmtpClient();
        await client.ConnectAsync("smtp.seznam.cz", 465, true);
        await client.AuthenticateAsync("atnet2019@seznam.cz", "Csharp2023");

        await client.SendAsync(msg);
        await client.DisconnectAsync(true);



        //using var client = new HttpClient();
        //using var request = new HttpRequestMessage(HttpMethod.Post, url);
        //request.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer " + "your_token_here");
        //request.Content = new StringContent("{bruh}", Encoding.UTF8, "application/json");

        //request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //{
        //  { "lon", "18.160005506399536" },
        //  { "lat", "49.831015379859586" },
        //  { "ac", "0" },
        //  { "unit", "metric" },
        //  { "output", "json" },
        //  { "tzshift", "0" }
        //});

        //using var response = await client.SendAsync(request);

        //response.EnsureSuccessStatusCode();

        //foreach (var header in response.Headers)
        //{
        //  Console.WriteLine(header.Key + ": " + header.Value.First());
        //}

        //var content = await response.Content.ReadAsStringAsync();
        ////Console.WriteLine(content);

        ////using var stream = await response.Content.ReadAsStreamAsync();
        ////using var fs = new FileStream("weather.json", FileMode.Create, FileAccess.Write);
        ////await stream.CopyToAsync(fs);

        ////Console.WriteLine("Weather data saved to weather.json");

        //var weather = JsonSerializer.Deserialize<Weather>(content);
        //if (weather == null) throw new Exception("Failed to deserialize weather data");

        //Console.WriteLine("Init: " + weather.init);
        //foreach (var data in weather.dataseries)
        //{
        //  Console.WriteLine("Timepoint: " + data.timepoint + " Temperature: " + data.temp2m);
        //}
       

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

    }
  }

}
