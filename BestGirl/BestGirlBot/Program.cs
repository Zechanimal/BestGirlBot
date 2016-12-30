using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Newtonsoft.Json;
using BestGirlBot.Discord.Gateway;

namespace BestGirlBot
{
	class Program
	{
		static void Main(string[] args)
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri("https://discordapp.com/api/");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", ConfigurationManager.AppSettings["AppBotToken"].ToString());
			client.DefaultRequestHeaders.UserAgent.ParseAdd("DiscordBot (BestGirlBot, 0.1)");

			// Example endpoint
			var response = client.GetAsync("gateway").Result;
			var body = response.Content.ReadAsStringAsync().Result;

			var getGatewayResponseDefinition = new { url = "" };
			var obj = JsonConvert.DeserializeAnonymousType(body, getGatewayResponseDefinition);
			Console.WriteLine($"Gateway url: {obj.url}");

			GatewaySocketClient socket = new GatewaySocketClient();

			var task = socket.Connect(obj.url, CancellationToken.None);
			task.Wait();

			Console.ReadLine();
		}
	}
}
