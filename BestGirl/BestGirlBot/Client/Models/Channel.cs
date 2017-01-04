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
			Voice,
			Private
		}

		public BestGirlClient Client { get; }

		public ulong Id { get; private set; }
		public Guild Guild { get; private set; }
		public string Name { get; private set; }
		public string Topic { get; private set; }
		public User Recipient { get; private set; }
		public Types Type { get; private set; }

		public Channel(BestGirlClient client, ulong id, Guild guild, string name, Types type, string topic)
		{
			Client = client;
			Id = id;
			Guild = guild;
			Name = name;
			Type = type;
			Recipient = null;
			Topic = topic;
		}

		public Channel(BestGirlClient client, ulong id, User recipient)
		{
			Client = client;
			Id = id;
			Guild = null;
			Name = null;
			Type = Types.Private;
			Recipient = recipient;
		}

		public void Update(string name, string topic)
		{
			Name = name;
			Topic = topic;
		}

		public async Task<Discord.Models.Message> SendMessage(string message)
		{
			return await Client.RestClient.CreateMessageAsync(Id, message);
		}
	}
}
