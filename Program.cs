using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace webapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddCommandLine(args)
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("hosting.json")
            .Build();

            var host = new WebHostBuilder()
               .UseKestrel()
               .UseConfiguration(config)
               //.UseUrls("http://localhost:5006")
               .Configure(app =>
               {
                   var dad = config["Family:Dad"];
                   app.Run(async (context) =>
                    { 
                        var person = context.Request.Path.Value.Replace("/","");
                        await context.Response.WriteAsync($"<h1>Hello World {person} his father is {dad}</h1>");
                    });
               })
               .Build();
            host.Run();
        }
    }
}
