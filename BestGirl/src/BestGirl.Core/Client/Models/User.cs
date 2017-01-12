using System.Collections.Generic;
using System.Linq;

namespace BestGirl.Core.Client.Models
{
	public class User
	{
		public BestGirlClient Client { get; }

		public ulong Id { get; private set; }
		public string Username { get; private set; }

		public User(BestGirlClient client, ulong id, string username)
		{
			Client = client;
			Id = id;
			Username = username;
		}

		public string Mention => Discord.Models.Message.MentionUser(Id);
	}
}
