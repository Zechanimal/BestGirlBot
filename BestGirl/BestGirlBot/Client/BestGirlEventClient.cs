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
using System.Collections.Generic;

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
			Console.WriteLine(gatewayMessage.Type);
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

						foreach (var channel in data.PrivateChannels)
						{
							var clientUser = AddOrGetUser(channel.Recipient);
							var clientChannel = new Models.Channel(this, channel.Id, clientUser);
							_dmChannels[clientChannel.Id] = clientChannel;
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
						var channel = eventMessage.EventData();
						Models.Channel clientChannel = null;

						if (channel.IsPrivate)
						{
							var recipient = AddOrGetUser(channel.Recipient);

							clientChannel = new Models.Channel(this, channel.Id, recipient);
							_dmChannels[clientChannel.Id] = clientChannel;
						}
						else
						{
							Models.Channel.Types type = channel.Type == Discord.Models.Channel.Types.Voice
								? Models.Channel.Types.Voice
								: Models.Channel.Types.Text;

							var guild = Guilds.First(g => g.Id == channel.GuildId);
							clientChannel = new Models.Channel(this, channel.Id, guild, channel.Name, type, channel.Topic);
							_guildChannels[clientChannel.Id] = clientChannel;
						}

						ChannelCreate(this, new ChannelEventArgs(clientChannel));
						break;
					}
				case GatewayEvent.ChannelUpdate:
					{
						var eventMessage = ChannelUpdateEvent.FromGatewayMessage(gatewayMessage);
						var channel = eventMessage.EventData();
						var clientChannel = (channel.IsPrivate ? DmChannels : GuildChannels).First(c => c.Id == channel.Id);

						clientChannel.Update(channel.Name, channel.Topic);
						ChannelUpdate(this, new ChannelEventArgs(clientChannel));
						break;
					}
				case GatewayEvent.ChannelDelete:
					{
						var eventMessage = ChannelDeleteEvent.FromGatewayMessage(gatewayMessage);
						var channel = eventMessage.EventData();
						var clientChannel = (channel.IsPrivate ? DmChannels : GuildChannels).First(c => c.Id == channel.Id);

						ChannelDelete(this, new ChannelEventArgs(clientChannel));
						// do delete
						break;
					}
				case GatewayEvent.GuildCreate:
					{
						var eventMessage = GuildCreateEvent.FromGatewayMessage(gatewayMessage);
						var guild = eventMessage.EventData();
						var guildId = guild.Id;
						var clientGuild = _guilds[guildId];

						foreach (var role in guild.Roles)
						{
							_roles[role.Id] = new Models.Role(this, role.Id, clientGuild, role.Name);
						}

						List<Models.Member> guildMembers = new List<Models.Member>();
						foreach (var member in guild.Members)
						{
							var user = member.User;
							var clientUser = AddOrGetUser(user);
							var clientRoles = member.RoleIds.Select(rid => Roles.First(r => r.Id == rid));

							var clientMember = new Models.Member(this, clientUser, clientGuild, member.Nick, member.Mute, member.Deaf, clientRoles);
							guildMembers.Add(clientMember);
						}

						foreach (var channel in guild.Channels)
						{
							var type = channel.Type == Discord.Models.Channel.Types.Voice ? Models.Channel.Types.Voice : Models.Channel.Types.Text;
							var guildChannel = new Models.Channel(this, channel.Id, clientGuild, channel.Name, type, channel.Topic);
							_guildChannels[guildChannel.Id] = guildChannel;
						}

						var owner = guildMembers.First(m => m.User.Id == guild.OwnerId);
						clientGuild.Create(guildMembers, guild.Name, !guild.Unavailable, owner);

						GuildCreate(this, new GuildEventArgs(clientGuild));
						break;
					}
				case GatewayEvent.GuildUpdate:
					{
						var eventMessage = GuildUpdateEvent.FromGatewayMessage(gatewayMessage);
						var guild = eventMessage.EventData();
						var clientGuild = Guilds.First(g => g.Id == guild.Id);

						clientGuild.Update();
						GuildUpdate(this, new GuildEventArgs(clientGuild));
						break;
					}
				case GatewayEvent.GuildDelete:
					{
						var eventMessage = GuildDeleteEvent.FromGatewayMessage(gatewayMessage);
						var guild = eventMessage.EventData();
						var clientGuild = Guilds.First(g => g.Id == guild.Id);

						GuildDelete(this, new GuildEventArgs(clientGuild));
						// do delete
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
						var message = eventMessage.EventData();
						var clientAuthor = Users.First(u => u.Id == message.Author.Id);
						Models.Channel clientChannel = null;

						if (_dmChannels.ContainsKey(message.ChannelId)) clientChannel = _dmChannels[message.ChannelId];
						else if (_guildChannels.ContainsKey(message.ChannelId)) clientChannel = _guildChannels[message.ChannelId];

						var clientMessage = new Models.Message(clientAuthor, clientChannel, message.Content, message.Id);

						MessageCreate(this, new MessageEventArgs(clientMessage));
						break;
					}
				case GatewayEvent.MessageUpdate:
					{
						var eventMessage = MessageCreateEvent.FromGatewayMessage(gatewayMessage);
						var message = eventMessage.EventData();
						var clientAuthor = Users.First(u => u.Id == message.Author.Id);
						Models.Channel clientChannel = null;

						if (_dmChannels.ContainsKey(message.ChannelId)) clientChannel = _dmChannels[message.ChannelId];
						else if (_guildChannels.ContainsKey(message.ChannelId)) clientChannel = _guildChannels[message.ChannelId];

						var clientMessage = new Models.Message(clientAuthor, clientChannel, message.Content, message.Id);

						MessageUpdate(this, new MessageEventArgs(clientMessage));
						break;
					}
				case GatewayEvent.MessageDelete:
					{
						var eventMessage = MessageCreateEvent.FromGatewayMessage(gatewayMessage);
						var message = eventMessage.EventData();
						var clientAuthor = Users.First(u => u.Id == message.Author.Id);
						Models.Channel clientChannel = null;

						if (_dmChannels.ContainsKey(message.ChannelId)) clientChannel = _dmChannels[message.ChannelId];
						else if (_guildChannels.ContainsKey(message.ChannelId)) clientChannel = _guildChannels[message.ChannelId];

						var clientMessage = new Models.Message(clientAuthor, clientChannel, message.Content, message.Id);

						MessageDelete(this, new MessageEventArgs(clientMessage));
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
