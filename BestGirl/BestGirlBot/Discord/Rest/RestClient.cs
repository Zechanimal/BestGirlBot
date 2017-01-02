using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BestGirlBot.Discord.Models;
using BestGirlBot.Discord.Rest.Repositories;
using BestGirlBot.Discord.Rest.Repositories.Interfaces;
using System;

namespace BestGirlBot.Discord.Rest
{
	public class RestClient : BaseRestRepository, IChannelRepository, IGatewayRepository, IUserRepository, IGuildRepository
	{
		public RestClient(string baseUri, string authToken, string userAgent) : base(baseUri, authToken, userAgent)
		{
		}

		public async Task<bool> BulkDeleteMessagesAsync(ulong channelId, ulong[] messageIds)
		{
			var response = await PostJsonAsync($"channels/{channelId}/messages/bulk-delete", new { messages = messageIds.Select(i => i.ToString()).ToArray() });
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<Message> CreateMessageAsync(ulong channelId, string content)
		{
			return await PostJsonAsync<Message>($"channels/{channelId}/messages", new { content = content });
		}

		public async Task<DMChannel> CreateDmAsync(ulong recipientId)
		{
			return await PostJsonAsync<DMChannel>($"users/@me/channels", new { recipient_id = recipientId });
		}

		public async Task<DMChannel> CreateGroupDmAsync(string[] accessTokens, IDictionary<ulong, string> nicks)
		{
			return await PostJsonAsync<DMChannel>($"users/@me/channels", new { access_tokens = accessTokens, nicks = nicks });
		}

		public async Task<Channel> DeleteChannelAsync(ulong channelId)
		{
			return await DeleteAsync<Channel>($"channels/{channelId}");
		}

		public async Task<bool> DeleteMessageAsync(ulong channelId, ulong messageId)
		{
			var response = await DeleteAsync($"channels/{channelId}/messages/{messageId}");
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<Message> EditMessageAsync(ulong channelId, ulong messageId, string content)
		{
			return await PostJsonAsync<Message>($"channels/{channelId}/messages/{messageId}", new { content = content });
		}

		public async Task<Channel> GetChannelAsync(ulong channelId)
		{
			var response = await GetAsync($"channels/{channelId}");
			var body = await response.Content.ReadAsStringAsync();
			var obj = JsonConvert.DeserializeObject(body) as JObject;
			var channel = obj.ToObject<Channel>();
			if (channel.IsPrivate)
			{
				return obj.ToObject<DMChannel>();
			}
			else
			{
				var guildChannel = obj.ToObject<GuildChannel>();
				return guildChannel.Type == "text" ? (Channel)obj.ToObject<GuildTextChannel>() : obj.ToObject<GuildVoiceChannel>();
			}
		}

		public async Task<Message> GetChannelMessageAsync(ulong channelId, ulong messageId)
		{
			return await GetAsync<Message>($"channels/{channelId}/messages/{messageId}");
		}

		public async Task<Message[]> GetChannelMessagesAroundAsync(ulong channelId, ulong messageId, int limit)
		{
			return await GetAsync<Message[]>($"channels/{channelId}/messages?around={messageId}&limit={limit}");
		}

		public async Task<Message[]> GetChannelMessagesBeforeAsync(ulong channelId, ulong messageId, int limit)
		{
			return await GetAsync<Message[]>($"channels/{channelId}/messages?before={messageId}&limit={limit}");
		}

		public async Task<Message[]> GetChannelMessagesAfterAsync(ulong channelId, ulong messageId, int limit)
		{
			return await GetAsync<Message[]>($"channels/{channelId}/messages?after={messageId}&limit={limit}");
		}

		public async Task<User> GetCurrentUserAsync()
		{
			return await GetAsync<User>($"users/@me");
		}

		public async Task<UserGuild[]> GetCurrentUserGuildsAsync()
		{
			return await GetAsync<UserGuild[]>($"users/@me/guilds");
		}

		public async Task<GatewayObject> GetGatewayAsync()
		{
			return await GetAsync<GatewayObject>($"gateway/bot");
		}

		public async Task<User> GetUserAsync(ulong userId)
		{
			return await GetAsync<User>($"users/{userId}");
		}

		public async Task<Connection[]> GetUserConnectionsAsync()
		{
			return await GetAsync<Connection[]>($"users/@me/connections");
		}

		public async Task<DMChannel[]> GetUserDmsAsync()
		{
			return await GetAsync<DMChannel[]>($"users/@me/channels");
		}

		public async Task<bool> LeaveGuildAsync(ulong guildId)
		{
			var response =  await DeleteAsync($"users/@me/guilds/{guildId}");
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<GuildChannel> ModifyChannelAsync(ulong channelId, string name, int position)
		{
			var data = new { name = name, position = position };
			return await PatchJsonAsync<GuildChannel>($"channels/{channelId}", data);
		}

		public async Task<GuildTextChannel> ModifyChannelAsync(ulong channelId, string name, int position, string topic)
		{
			var data = new { name = name, position = position, topic = topic };
			return await PatchJsonAsync<GuildTextChannel>($"channels/{channelId}", data);
		}

		public async Task<GuildVoiceChannel> ModifyChannelAsync(ulong channelId, string name, int position, int bitrate, int userLimit)
		{
			var data = new { name = name, position = position, bitrate = bitrate, user_limit = userLimit };
			return await PatchJsonAsync<GuildVoiceChannel>($"channels/{channelId}", data);
		}

		public async Task<User> ModifyCurrentUserAsync(string username, string avatarData)
		{
			Dictionary<string, string> data = new Dictionary<string, string>();
			if (username != null) data.Add("username", username);
			if (avatarData != null) data.Add("avatar", avatarData);

			return await PatchJsonAsync<User>("users/@me", data);
		}

		public async Task<Guild> GetGuildAsync(ulong guildId)
		{
			return await GetAsync<Guild>($"guilds/{guildId}");
		}

		public async Task<Guild> ModifyGuildAsync(ulong guildId, string name = null, string region = null, int? verificationLevel = default(int?), int? defaultMessageNotifications = default(int?),
			ulong? afkChannelId = default(ulong?), int? afkTimeout = default(int?), string icon = null, ulong? ownerId = default(ulong?), string splash = null)
		{
			Dictionary<string, object> data = new Dictionary<string, object>();
			if (name != null) data.Add("name", name);
			if (region != null) data.Add("region", region);
			if (verificationLevel.HasValue) data.Add("verification_level", verificationLevel.Value);
			if (defaultMessageNotifications.HasValue) data.Add("default_message_notifications", defaultMessageNotifications.Value);
			if (afkChannelId.HasValue) data.Add("afk_channel_id", afkChannelId.Value);
			if (afkTimeout.HasValue) data.Add("afk_timeout", afkTimeout.Value);
			if (icon != null) data.Add("icon", icon);
			if (ownerId.HasValue) data.Add("owner_id", ownerId.Value);
			if (splash != null) data.Add("splash", splash);

			return await PatchJsonAsync<Guild>($"guilds/{guildId}", data);
		}

		public async Task<Guild> DeleteGuildAsync(ulong guildId)
		{
			return await DeleteAsync<Guild>($"guilds/{guildId}");
		}

		public async Task<GuildChannel[]> GetGuildChannelsAsync(ulong guildId)
		{
			return await GetAsync<GuildChannel[]>($"guilds/{guildId}/channels");
		}

		public async Task<GuildTextChannel> CreateGuildTextChannelAsync(ulong guildId, string name, Permissions[] permissionOverwrites)
		{
			var data = new { name = name, type = GuildChannel.Types.Text, permission_overwrites = permissionOverwrites };
			return await PostJsonAsync<GuildTextChannel>($"guilds/{guildId}/channels", data);
		}

		public async Task<GuildVoiceChannel> CreateGuildVoiceChannelAsync(ulong guildId, string name, int bitrate, int userLimit, Permissions[] permissionOverwrites)
		{
			var data = new { name = name, type = GuildChannel.Types.Voice, bitrate = bitrate, user_limit = userLimit, permission_overwrites = permissionOverwrites };
			return await PostJsonAsync<GuildVoiceChannel>($"guilds/{guildId}/channels", data);
		}

		public async Task<GuildChannel[]> ModifyGuildChannelPositionsAsync(ulong guildId, Tuple<ulong, int>[] channelOrders)
		{
			List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
			foreach (var channelOrder in channelOrders)
			{
				data.Add(new Dictionary<string, object>() { { "id", channelOrder.Item1 }, { "position", channelOrder.Item2 } });
			}

			return await PatchJsonAsync<GuildChannel[]>($"guilds/{guildId}/channels", data);
		}

		public async Task<GuildMember> GetGuildMemberAsync(ulong guildId, ulong userId)
		{
			return await GetAsync<GuildMember>($"guilds/{guildId}/members/{userId}");
		}

		public async Task<GuildMember[]> ListGuildMembersAsync(ulong guildId, int limit = 1, int after = 0)
		{
			return await GetAsync<GuildMember[]>($"guilds/{guildId}/members?limit={limit}&after={after}");
		}

		public async Task<GuildMember> AddGuildMemberAsync(ulong guildId, ulong userId, string accessToken, string nick, Role[] roles, bool mute = false, bool deaf = false)
		{
			var data = new { access_token = accessToken, nick = nick, roles = roles, mute = mute, deaf = deaf };
			return await PutJsonAsync<GuildMember>($"guilds/{guildId}/members/{userId}", data);
		}

		public async Task<bool> ModifyGuildMemberAsync(ulong guildId, ulong userId, string nick = null, Role[] roles = null, bool? mute = default(bool?), bool? deaf = default(bool?), ulong? voiceChannelId = default(ulong?))
		{
			Dictionary<string, object> data = new Dictionary<string, object>();
			if (nick != null) data.Add("nick", nick);
			if (roles != null) data.Add("roles", roles);
			if (mute.HasValue) data.Add("mute", mute);
			if (deaf.HasValue) data.Add("deaf", deaf);
			if (voiceChannelId.HasValue) data.Add("channel_id", voiceChannelId);

			var response = await PatchJsonAsync($"guilds/{guildId}/members/{userId}", data);
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<bool> RemoveGuildMemberAsync(ulong guildId, ulong userId)
		{
			var response = await DeleteAsync($"guilds/{guildId}/members/{userId}");
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<User[]> GetGuildBansAsync(ulong guildId)
		{
			return await GetAsync<User[]>($"guilds/{guildId}/bans");
		}

		public async Task<bool> CreateGuildBanAsync(ulong guildId, ulong userId, int deleteMessageDays = 0)
		{
			var data = new Dictionary<string, object>() { { "delete-message-days", deleteMessageDays } };
			var response = await PutJsonAsync($"guilds/{guildId}/bans/{userId}", data);
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<bool> RemoveGuildBanAsync(ulong guildId, ulong userId)
		{
			var response = await DeleteAsync($"guilds/{guildId}/bans/{userId}");
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<Role[]> GetGuildRolesAsync(ulong guildId)
		{
			return await GetAsync<Role[]>($"guilds/{guildId}/roles");
		}

		public async Task<Role[]> ModifyGuildRolePositionsAsync(ulong guildId, Tuple<ulong, int>[] roleOrders)
		{
			List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
			foreach (var roleOrder in roleOrders)
			{
				data.Add(new Dictionary<string, object>() { { "id", roleOrder.Item1 }, { "position", roleOrder.Item2 } });
			}

			return await PatchJsonAsync<Role[]>($"guilds/{guildId}/roles", data);
		}

		public async Task<Role> ModifyGuildRoleAsync(ulong guildId, ulong roleId, string name, Permissions permissions, int position, int color, bool hoist = true, bool mentionable = true)
		{
			var data = new
			{
				name = name,
				permissions = permissions,
				position = position,
				color = color,
				hoist = hoist,
				mentionable = mentionable
			};

			return await PatchJsonAsync<Role>($"guilds/{guildId}/roles/{roleId}", data);
		}

		public async Task<Role> DeleteGuildRoleAsync(ulong guildId, ulong roleId)
		{
			return await DeleteAsync<Role>($"guilds/{guildId}/roles/{roleId}");
		}

		public async Task<int> GetGuildPruneCountAsync(ulong guildId, int days)
		{
			var responseData = new { pruned = 0 };
			var response = await GetAsync($"guilds/{guildId}/prune?days={days}");
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeAnonymousType(content, responseData).pruned;
		}

		public async Task<int> BeginGuildPruneAsync(ulong guildId, int days)
		{
			var data = new { days = days };
			var responseData = new { pruned = 0 };
			var response = await PostJsonAsync($"guilds/{guildId}/prune", data);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeAnonymousType(content, responseData).pruned;
		}

		public async Task<VoiceRegion[]> GetGuildVoiceRegionsAsync(ulong guildId)
		{
			return await GetAsync<VoiceRegion[]>($"guilds/{guildId}/regions");
		}

		public async Task<InviteWithMetadata[]> GetGuildInvitesAsync(ulong guildId)
		{
			return await GetAsync<InviteWithMetadata[]>($"guilds/{guildId}/invites");
		}

		public async Task<Integration[]> GetGuildIntegrationsAsync(ulong guildId)
		{
			return await GetAsync<Integration[]>($"guilds/{guildId}/integrations");
		}

		public async Task<bool> CreateGuildIntegrationAsync(ulong guildId, string type, ulong integrationId)
		{
			var data = new { type = type, id = integrationId };
			var response = await PostJsonAsync($"guilds/{guildId}/integrations", data);
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<bool> ModifyGuildIntegrationAsync(ulong guildId, ulong integrationId, int expireBehavior, int expireGracePeriodSeconds, bool enableEmoticons)
		{
			var data = new { expire_behavior = expireBehavior, expire_grace_period = expireGracePeriodSeconds, enable_emoticons = enableEmoticons };
			var response = await PatchJsonAsync($"guilds/{guildId}/integrations/{integrationId}", data);
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<bool> DeleteGuildIntegrationAsync(ulong guildId, ulong integrationId)
		{
			var response = await DeleteAsync($"guilds/{guildId}/integrations/{integrationId}");
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<bool> SyncGuildIntegrationAsync(ulong guildId, ulong integrationId)
		{
			var response = await PostJsonAsync($"guilds/{guildId}/integrations/{integrationId}/sync", null);
			return response.StatusCode == System.Net.HttpStatusCode.NoContent;
		}

		public async Task<GuildEmbed> GetGuildEmbedAsync(ulong guildId)
		{
			return await GetAsync<GuildEmbed>($"guilds/{guildId}/embed");
		}

		public async Task<GuildEmbed> ModifyGuildEmbedAsync(ulong guildId, bool? enabled = default(bool?), ulong? channelId = default(ulong?))
		{
			Dictionary<string, object> data = new Dictionary<string, object>();
			if (enabled.HasValue) data.Add("enabled", enabled.Value);
			if (channelId.HasValue) data.Add("channel_id", channelId.Value);

			return await PatchJsonAsync<GuildEmbed>($"guilds/{guildId}/embed", data);
		}
	}
}
