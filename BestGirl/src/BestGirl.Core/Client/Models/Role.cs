﻿namespace BestGirl.Core.Client.Models
{
	public class Role
	{
		public BestGirlClient Client { get; }

		public ulong Id { get; private set; }
		public Guild Guild { get; private set; }
		public string Name { get; private set; }

		public string Mention => Discord.Models.Message.MentionRole(Id);

		public Role(BestGirlClient client, ulong id, Guild guild, string name)
		{
			Client = client;
			Id = id;
			Guild = guild;
			Name = name;
		}
	}
}
