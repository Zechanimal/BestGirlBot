using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Newtonsoft.Json;
using BestGirlBot.Discord.Gateway;
using BestGirlBot.Discord.Gateway.Messages;
using BestGirlBot.Discord.Gateway.Payloads;

namespace BestGirlBot
{
	class Program
	{
		static string AuthToken { get; set; }

		static void Main(string[] args)
		{
			AuthToken = ConfigurationManager.AppSettings["AppBotToken"].ToString();
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri("https://discordapp.com/api/");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", AuthToken);
			client.DefaultRequestHeaders.UserAgent.ParseAdd("DiscordBot (BestGirlBot, 0.1)");

			// Example endpoint
			var response = client.GetAsync("gateway").Result;
			var body = response.Content.ReadAsStringAsync().Result;

			var getGatewayResponseDefinition = new { url = "" };
			var obj = JsonConvert.DeserializeAnonymousType(body, getGatewayResponseDefinition);
			Console.WriteLine($"Gateway url: {obj.url}");

			GatewaySocketClient socket = new GatewaySocketClient();
			socket.GatewayMessageReceived += HandleGatewayMessage;
			socket.Connect(obj.url, CancellationToken.None).Wait();

			Console.ReadLine();
		}

		static void HandleGatewayMessage(object sender, GatewayMessageEventArgs e)
		{
			var socket = sender as GatewaySocketClient;
			var gatewayMessage = e.Message;

			switch (gatewayMessage.OpCode)
			{
				case GatewayOpCode.Dispatch:
					break;
				case GatewayOpCode.Heartbeat:
					break;
				case GatewayOpCode.Identify:
					break;
				case GatewayOpCode.StatusUpdate:
					break;
				case GatewayOpCode.VoiceStateUpdate:
					break;
				case GatewayOpCode.VoiceServerPing:
					break;
				case GatewayOpCode.Resume:
					break;
				case GatewayOpCode.Reconnect:
					break;
				case GatewayOpCode.RequestGuildMembers:
					break;
				case GatewayOpCode.InvalidSession:
					break;
				case GatewayOpCode.Hello:
					Console.WriteLine($"Received Hello with heartbeat {gatewayMessage.DataAs<HelloPayload>().HeartbeatInterval}");
					socket.SendMessage(new Identify(AuthToken, null, false, 50));
					break;
				case GatewayOpCode.HeartbeatACK:
					break;
				default:
					break;
			}
		}
	}
}
