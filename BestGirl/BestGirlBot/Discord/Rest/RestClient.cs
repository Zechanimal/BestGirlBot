using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestGirlBot.Discord.Models;
using BestGirlBot.Discord.Rest.Repositories;
using BestGirlBot.Discord.Rest.Repositories.Interfaces;
using Newtonsoft.Json;

namespace BestGirlBot.Discord.Rest
{
	public class RestClient : BaseRestRepository, IChannelRepository, IGatewayRepository, IUserRepository
	{
		public RestClient(string baseUri, string authToken, string userAgent) : base(baseUri, authToken, userAgent)
		{
		}

		public Task BulkDeleteMessagesAsync(ulong channelId, ulong[] messageIds)
		{
			throw new NotImplementedException();
		}

		public Task CreateChannelMessageAsync(ulong channelId, string content)
		{
			throw new NotImplementedException();
		}

		public Task<DMChannel> CreateDmAsync(ulong recipientId)
		{
			throw new NotImplementedException();
		}

		public Task<DMChannel> CreateGroupDmAsync(string[] accessTokens, IDictionary<ulong, string> nicks)
		{
			throw new NotImplementedException();
		}

		public Task DeleteChannelAsync(ulong channelId)
		{
			throw new NotImplementedException();
		}

		public Task DeleteMessageAsync(ulong channelId, ulong messageId)
		{
			throw new NotImplementedException();
		}

		public Task EditMessageAsync(ulong channelId, ulong messageId, string content)
		{
			throw new NotImplementedException();
		}

		public async Task<Channel> GetChannelAsync(ulong channelId)
		{
			return await GetAsync<Channel>($"channels/{channelId}");
		}

		public async Task<Message> GetChannelMessageAsync(ulong channelId, ulong messageId)
		{
			return await GetAsync<Message>($"channels/{channelId}/messages/{messageId}");
		}

		public Task<Message[]> GetChannelMessagesAsync(ulong channelId)
		{
			throw new NotImplementedException();
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

		public Task LeaveGuildAsync(ulong guildId)
		{
			throw new NotImplementedException();
		}

		public Task ModifyChannelAsync(ulong channelId, string name, int position)
		{
			throw new NotImplementedException();
		}

		public Task ModifyChannelAsync(ulong channelId, string name, int position, string topic)
		{
			throw new NotImplementedException();
		}

		public Task ModifyChannelAsync(ulong channelId, string name, int position, int bitrate, int userLimit)
		{
			throw new NotImplementedException();
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
