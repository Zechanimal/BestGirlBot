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
	public class RestClient : BaseRestRepository, IChannelRepository, IGatewayRepository
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

		public GatewayObject GetGateway()
		{
			var response = GetAsync("gateway").Result;
			var body = response.Content.ReadAsStringAsync().Result;

			return JsonConvert.DeserializeObject<GatewayObject>(body);
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
	}
}
