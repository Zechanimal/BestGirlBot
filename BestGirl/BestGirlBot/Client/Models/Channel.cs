using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BestGirlBot.Client.Models
{
	public class Channel
	{
		public enum Types
		{
			Text,
			Voice
		}

		public ulong Id { get; private set; }
		public Guild Guild { get; private set; }
		public string Name { get; private set; }
		public Types Type { get; private set; }

		public Channel(ulong id, Guild guild, string name, Types type)
		{
			Id = id;
			Guild = guild;
			Name = name;
			Type = type;
		}
	}
}
