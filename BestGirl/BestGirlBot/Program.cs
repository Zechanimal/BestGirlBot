using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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
			var response = client.GetAsync("users/@me").Result;
			var body = response.Content.ReadAsStringAsync().Result;
			var user = JsonConvert.DeserializeObject<BestGirlBot.Discord.Models.User>(body);
			Console.WriteLine(user.Username);

			Console.ReadLine();
		}
	}
}
