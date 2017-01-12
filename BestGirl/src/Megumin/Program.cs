using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using BestGirl.Core.Client;
using System.Threading.Tasks;

namespace Megumin
{
	public class Program
	{
		static readonly string HostingEnvironmentKey = "ASPNETCORE_ENVIRONMENT";
		static string HostingEnvironment { get; set; }
		static IConfigurationRoot Configuration { get; set; }

		static string AuthToken { get; set; }

		public static void Main(string[] args)
		{
			HostingEnvironment = Environment.GetEnvironmentVariable(HostingEnvironmentKey);
			Configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{HostingEnvironment}.json")
				.AddEnvironmentVariables()
				.Build();

			AuthToken = Configuration["Megumin:Client:AuthToken"].ToString();
			Console.WriteLine(AuthToken);

			BestGirlClient client = new BestGirlClient(new BestGirlConfig
			{
				AuthToken = AuthToken,
				UserAgent = "DiscordBot (Megumin, 0.1)"
			});

			Task.Run(async () => await client.Connect());
			Console.ReadLine();
		}
	}
}
