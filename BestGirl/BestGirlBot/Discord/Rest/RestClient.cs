using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BestGirlBot.Discord.Models;
using BestGirlBot.Discord.Rest.Repositories;
using BestGirlBot.Discord.Rest.Repositories.Interfaces;

namespace BestGirlBot.Discord.Rest
{
	public class RestClient : BaseRestRepository, IChannelRepository, IGatewayRepository, IUserRepository
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
	}
}
