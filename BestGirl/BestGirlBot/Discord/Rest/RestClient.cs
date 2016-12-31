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

		public void BulkDeleteMessages(ulong channelId, ulong[] messageIds)
		{
			throw new NotImplementedException();
		}

		public void CreateChannelMessage(ulong channelId, string content)
		{
			throw new NotImplementedException();
		}

		public DMChannel CreateDM(ulong recipientId)
		{
			throw new NotImplementedException();
		}

		public DMChannel CreateGroupDM(string[] accessTokens, IDictionary<ulong, string> nicks)
		{
			throw new NotImplementedException();
		}

		public void DeleteChannel(ulong channelId)
		{
			throw new NotImplementedException();
		}

		public void DeleteMessage(ulong channelId, ulong messageId)
		{
			throw new NotImplementedException();
		}

		public void EditMessage(ulong channelId, ulong messageId, string content)
		{
			throw new NotImplementedException();
		}

		public Channel GetChannel(ulong channelId)
		{
			throw new NotImplementedException();
		}

		public Message GetChannelMessage(ulong channelId, ulong messageId)
		{
			throw new NotImplementedException();
		}

		public Message[] GetChannelMessages(ulong channelId)
		{
			throw new NotImplementedException();
		}

		public User GetCurrentUser()
		{
			return GetAsync<User>("users/@me").Result;
		}

		public UserGuild[] GetCurrentUserGuilds()
		{
			throw new NotImplementedException();
		}

		public GatewayObject GetGateway()
		{
			return GetAsync<GatewayObject>("gateway/bot").Result;
		}

		public User GetUser(ulong userId)
		{
			throw new NotImplementedException();
		}

		public Connection[] GetUserConnections()
		{
			throw new NotImplementedException();
		}

		public DMChannel[] GetUserDMs()
		{
			throw new NotImplementedException();
		}

		public void LeaveGuild(ulong guildId)
		{
			throw new NotImplementedException();
		}

		public void ModifyChannel(ulong channelId, string name, int position)
		{
			throw new NotImplementedException();
		}

		public void ModifyChannel(ulong channelId, string name, int position, string topic)
		{
			throw new NotImplementedException();
		}

		public void ModifyChannel(ulong channelId, string name, int position, int bitrate, int userLimit)
		{
			throw new NotImplementedException();
		}

		public User ModifyCurrentUser(string username, string avatarData)
		{
			Dictionary<string, string> data = new Dictionary<string, string>();
			if (username != null) data.Add("username", username);
			if (avatarData != null) data.Add("avatar", avatarData);

			return PatchJsonAsync<User>("users/@me", data).Result;
		}
	}
}
