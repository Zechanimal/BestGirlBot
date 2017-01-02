using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BestGirlBot.Discord.Gateway;
using BestGirlBot.Discord.Gateway.Payloads;
using BestGirlBot.Discord.Gateway.Messages;
using BestGirlBot.Discord.Models;
using BestGirlBot.Discord.Rest;
using Newtonsoft.Json;
using System.IO;

namespace BestGirlBot.Client
{
	public class BestGirlClient
	{
		public BestGirlConfig Config { get; }
		public GatewaySocketClient GatewaySocketClient { get; }
		public RestClient RestClient { get; }
		private CancellationToken GatewayCancelToken { get; } = CancellationToken.None;

		private static readonly string DiscordHttpApiBaseUri = "https://discordapp.com/api/";

		private int _shardCount { get; set; } = 0;
		private int? _previousSequence { get; set; } = null;
		private bool _handshakeCompleted = false;
		private int? _heartbeatInterval = null;

		private ulong? _botUserId = null;

		private ConcurrentDictionary<ulong, Models.Guild> _guilds;

		public BestGirlClient(BestGirlConfig config)
		{
			Config = config;
			RestClient = new RestClient(DiscordHttpApiBaseUri, Config.AuthToken, Config.UserAgent);
			_botUserId = RestClient.GetCurrentUserAsync().Result.Id;

			GatewaySocketClient = new GatewaySocketClient();
			GatewaySocketClient.GatewayMessageReceived += HandleGatewayMessage;

			_guilds = new ConcurrentDictionary<ulong, Models.Guild>();
		}

		public async Task Connect()
		{
			var gatewayObject = RestClient.GetGatewayAsync().Result;
			var gatewayUrl = gatewayObject.Url;
			_shardCount = gatewayObject.Shards;

			await GatewaySocketClient.Connect(gatewayUrl, GatewayCancelToken);
		}

		private void HandleGatewayMessage(object sender, GatewayMessageEventArgs e)
		{
			var socket = sender as GatewaySocketClient;
			var gatewayMessage = e.Message;

			switch (gatewayMessage.OpCode)
			{
				case GatewayOpCode.Dispatch:
					HandleGatewayDispatchEvent(gatewayMessage);
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
					_heartbeatInterval = gatewayMessage.DataAs<HelloPayload>().HeartbeatInterval;
					socket.SendMessage(new Identify(Config.AuthToken, null, false, 50, 0, _shardCount));
					break;
				case GatewayOpCode.HeartbeatACK:
					break;
				default:
					break;
			}
		}

		private void HandleGatewayDispatchEvent(GatewayMessage message)
		{
			_previousSequence = message.Sequence;
			Console.WriteLine($"Message received of type {message.Type}");
			switch(message.Type)
			{
				case "READY":
					{
						_handshakeCompleted = true;
						var msgData = message.DataAs<ReadyPayload>();
						foreach (var guild in msgData.Guilds)
						{
							_guilds[guild.Id] = new Models.Guild(this, guild.Id);
						}

						Task.Run(async () => await SendHeartbeat());
						break;
					}
				case "MESSAGE_CREATE":
					{
						var msgData = message.DataAs<Message>();
						if (msgData.Author.Id != _botUserId)
						{
							if (msgData.Mentions.Where(u => u.Id == _botUserId).Count() > 0)
							{
								string authorMention = Message.MentionUser(msgData.Author.Id);
								string response = $"Stop poking me, {authorMention}. I'm not fully functional.";
								RestClient.CreateMessageAsync(msgData.ChannelId, response).Wait();
							}
						}
						break;
					}
				case "CHANNEL_CREATE":
					{
						var channel = message.DataAs<Channel>();
						if (channel.IsPrivate)
						{
							var dmChannel = message.DataAs<DMChannel>();
						}
						else
						{
							var guildChannel = message.DataAs<GuildChannel>();
							if (guildChannel.Type == GuildChannel.Types.Text)
								_guilds[guildChannel.GuildId].CreateTextChannel(message.DataAs<GuildTextChannel>());
							else
								_guilds[guildChannel.GuildId].CreateVoiceChannel(message.DataAs<GuildVoiceChannel>());
						}
						break;
					}
				case "GUILD_CREATE":
					{
						var guild = message.DataAs<Guild>();
						if (_guilds.ContainsKey(guild.Id))
						{
							_guilds[guild.Id].Create(guild);
						} else
						{
							_guilds[guild.Id] = new Models.Guild(this, guild.Id);
							_guilds[guild.Id].Create(guild);
						}
						break;
					}
				case "GUILD_UPDATE":
					{
						var guild = message.DataAs<Guild>();
						if (_guilds.ContainsKey(guild.Id))
						{
							_guilds[guild.Id].Update(guild);
						}
						break;
					}
				default:
					break;
			}
		}

		private async Task SendHeartbeat()
		{
			if (_handshakeCompleted && _heartbeatInterval != null)
			{
				DateTime lastHeartbeatTime = DateTime.Now;

				while (!GatewayCancelToken.IsCancellationRequested)
				{
					GatewaySocketClient.SendMessage(new Heartbeat(_previousSequence.Value));

					await Task.Delay(_heartbeatInterval.Value, GatewayCancelToken).ConfigureAwait(false);
				}
			}
		}
	}
}
