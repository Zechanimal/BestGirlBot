using System;
using System.Configuration;
using System.Threading.Tasks;
using BestGirlBot.Client;

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
