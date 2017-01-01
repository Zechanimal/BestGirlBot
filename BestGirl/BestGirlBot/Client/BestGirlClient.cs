﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BestGirlBot.Discord.Gateway;
using BestGirlBot.Discord.Gateway.Payloads;
using BestGirlBot.Discord.Gateway.Messages;
using BestGirlBot.Discord.Models;
using BestGirlBot.Discord.Rest;

namespace BestGirlBot.Client
{
	public class BestGirlClient
	{
		public BestGirlConfig Config { get; }
		private GatewaySocketClient GatewaySocketClient { get; }
		private RestClient RestClient { get; }
		private CancellationToken GatewayCancelToken { get; } = CancellationToken.None;

		private static readonly string DiscordHttpApiBaseUri = "https://discordapp.com/api/";

		private int _shardCount { get; set; } = 0;
		private int? _previousSequence { get; set; } = null;
		private bool _handshakeCompleted = false;
		private int? _heartbeatInterval = null;

		private ulong? _botUserId = null;

		public BestGirlClient(BestGirlConfig config)
		{
			Config = config;
			RestClient = new RestClient(DiscordHttpApiBaseUri, Config.AuthToken, Config.UserAgent);
			_botUserId = RestClient.GetCurrentUserAsync().Result.Id;

			GatewaySocketClient = new GatewaySocketClient();
			GatewaySocketClient.GatewayMessageReceived += HandleGatewayMessage;
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
			switch(message.Type)
			{
				case "READY":
					_handshakeCompleted = true;
					Task.Run(async () => await SendHeartbeat());
					break;
				case "MESSAGE_CREATE":
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