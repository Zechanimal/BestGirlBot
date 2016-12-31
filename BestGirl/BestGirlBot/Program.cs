using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BestGirlBot.Client;
using BestGirlBot.Discord.Gateway;
using BestGirlBot.Discord.Gateway.Messages;
using BestGirlBot.Discord.Gateway.Payloads;
using BestGirlBot.Discord.Rest;

namespace BestGirlBot
{
	class Program
	{
		static string AuthToken { get; set; }

		static void Main(string[] args)
		{
			AuthToken = ConfigurationManager.AppSettings["AppBotToken"].ToString();

			BestGirlClient client = new BestGirlClient(new BestGirlConfig
			{
				AuthToken = AuthToken,
				UserAgent = "DiscordBot (BestGirlBot, 0.1)"
			});

			Task.Run(async () => await client.Connect());
			Console.ReadLine();
		}
	}
}
