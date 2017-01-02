using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestGirlBot.Client.Models
{
	public class Channel
	{
		public enum Types
		{
			Text,
			Voice
		}

		public BestGirlClient Client { get; }

		public ulong Id { get; private set; }
		public Guild Guild { get; private set; }
		public string Name { get; private set; }
		public Types Type { get; private set; }

		public Channel(BestGirlClient client, ulong id, Guild guild, string name, Types type)
		{
			Client = client;
			Id = id;
			Guild = guild;
			Name = name;
			Type = type;
		}

		public async Task<Discord.Models.Message> SendMessage(string message)
		{
			return await Client.RestClient.CreateMessageAsync(Id, message);
		}
	}
}
