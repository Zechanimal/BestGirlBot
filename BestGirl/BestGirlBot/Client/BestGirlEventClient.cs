using System;
using System.Threading;
using System.Threading.Tasks;
using BestGirlBot.Client.Events;
using BestGirlBot.Discord.Gateway;
using BestGirlBot.Discord.Gateway.Events;
using BestGirlBot.Discord.Gateway.Messages;
using BestGirlBot.Discord.Gateway.Payloads;
using BestGirlBot.Extensions;
using System.Linq;

namespace BestGirlBot.Client
{
	public partial class BestGirlClient
	{
		public EventHandler<HeartbeatEventArgs> Heartbeat = delegate { };
		public EventHandler<ChannelEventArgs> ChannelCreate = delegate { };
		public EventHandler<ChannelEventArgs> ChannelUpdate = delegate { };
		public EventHandler<ChannelEventArgs> ChannelDelete = delegate { };
		public EventHandler<GuildEventArgs> GuildCreate = delegate { };
		public EventHandler<GuildEventArgs> GuildUpdate = delegate { };
		public EventHandler<GuildEventArgs> GuildDelete = delegate { };
		public EventHandler<GuildEventArgs> GuildMemberAdd = delegate { };
		public EventHandler<GuildEventArgs> GuildMemberUpdate = delegate { };
		public EventHandler<GuildEventArgs> GuildMemberRemove = delegate { };
		public EventHandler<UserEventArgs> UserUpdate = delegate { };
		public EventHandler<MessageEventArgs> MessageCreate = delegate { };
		public EventHandler<MessageEventArgs> MessageUpdate = delegate { };
		public EventHandler<MessageEventArgs> MessageDelete = delegate { };

		private void OnGatewayMessageReceived(object sender, GatewayMessageEventArgs e)
		{
			var socket = sender as GatewaySocketClient;
			var gatewayMessage = e.Message;

			switch (gatewayMessage.OpCode)
			{
				case GatewayOpCode.Dispatch:
					HandleGatewayDispatchMessage(gatewayMessage);
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

		private void HandleGatewayDispatchMessage(GatewayMessage gatewayMessage)
		{
			_previousSequence = gatewayMessage.Sequence;
			switch (gatewayMessage.Type.ToEnumFromDescription<GatewayEvent>())
			{
				case GatewayEvent.Ready:
					{
						var eventMessage = ReadyEvent.FromGatewayMessage(gatewayMessage);
						var data = eventMessage.EventData();
						_handshakeCompleted = true;
						foreach (var guild in data.Guilds)
						{
							_guilds[guild.Id] = new Models.Guild(this, guild.Id);
						}

						Task.Run(async () => await StartHeartbeat(GatewayCancelToken));
						break;
					}
				case GatewayEvent.Resumed:
					{
						var eventMessage = ResumedEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.ChannelCreate:
					{
						var eventMessage = ChannelCreateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.ChannelUpdate:
					{
						var eventMessage = ChannelUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.ChannelDelete:
					{
						var eventMessage = ChannelDeleteEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildCreate:
					{
						var eventMessage = GuildCreateEvent.FromGatewayMessage(gatewayMessage);
						var data = eventMessage.EventData();
						var guildId = data.Id;
						var guild = _guilds[guildId];

						foreach (var role in data.Roles)
						{
							_roles[role.Id] = new Models.Role(this, role.Id, guild, role.Name);
						}

						foreach (var member in data.Members)
						{
							var user = member.User;
							var roles = member.RoleIds.Select(rid => Roles.First(r => r.Id == rid));
							var guildMember = new Models.User(this, user.Id, user.Username, guild, member.Nick, member.Mute, member.Deaf, roles);
							_users[user.Id] = _users[guildMember.Id];
						}

						foreach (var channel in data.Channels)
						{
							var type = channel.Type == Discord.Models.Channel.Types.Voice ? Models.Channel.Types.Voice : Models.Channel.Types.Text;
							var guildChannel = new Models.Channel(this, channel.Id, guild, channel.Name, type);
							_guildChannels[guildChannel.Id] = guildChannel;
						}

						break;
					}
				case GatewayEvent.GuildUpdate:
					{
						var eventMessage = GuildUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildDelete:
					{
						var eventMessage = GuildDeleteEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildBanAdd:
					{
						var eventMessage = GuildBanAddEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildBanRemove:
					{
						var eventMessage = GuildBanRemoveEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildEmojisUpdate:
					{
						var eventMessage = GuildEmojisUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildIntegrationsUpdate:
					{
						// var eventMessage = GuildIntegrationsEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildMemberAdd:
					{
						var eventMessage = GuildMemberAddEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildMemberRemove:
					{
						var eventMessage = GuildMemberRemoveEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildMemberUpdate:
					{
						var eventMessage = GuildMemberUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildMembersChunk:
					{
						var eventMessage = GuildMembersChunkEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildRoleCreate:
					{
						var eventMessage = GuildRoleCreateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildRoleUpdate:
					{
						var eventMessage = GuildRoleUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.GuildRoleDelete:
					{
						var eventMessage = GuildRoleDeleteEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.MessageCreate:
					{
						var eventMessage = MessageCreateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.MessageUpdate:
					{
						var eventMessage = MessageUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.MessageDelete:
					{
						var eventMessage = MessageDeleteEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.MessageDeleteBulk:
					{
						var eventMessage = MessageDeleteBulkEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.PresenceUpdate:
					{
						var eventMessage = PresenceUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.TypingStart:
					{
						var eventMessage = TypingStartEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.UserSettingsUpdate:
					{
						// var eventMessage = UserSettingsUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.UserUpdate:
					{
						var eventMessage = UserUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.VoiceStateUpdate:
					{
						var eventMessage = VoiceStateUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				case GatewayEvent.VoiceServerUpdate:
					{
						var eventMessage = VoiceServerUpdateEvent.FromGatewayMessage(gatewayMessage);
						break;
					}
				default:
					break;
			}
		}

		private async Task StartHeartbeat(CancellationToken cancellationToken)
		{
			if (_handshakeCompleted && _heartbeatInterval != null)
			{
				DateTime previous = DateTime.Now;

				while (!cancellationToken.IsCancellationRequested)
				{
					DateTime now = DateTime.Now;
					var interval = (now - previous).TotalMilliseconds;
					bool serverHeartbeat = interval >= _heartbeatInterval;

					if (serverHeartbeat)
					{
						GatewaySocketClient.SendMessage(new Heartbeat(_previousSequence.Value));
					}

					Heartbeat(this, new HeartbeatEventArgs(previous, now, serverHeartbeat));

					previous = now;
					await Task.Delay(100, cancellationToken).ConfigureAwait(false);
				}
			}
		}
	}
}
