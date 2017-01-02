﻿using System.Collections.Generic;
using System.Linq;

namespace BestGirlBot.Client.Models
{
	public class User
	{
		public BestGirlClient Client { get; }

		public ulong Id { get; private set; }
		public Guild Guild { get; private set; }
		public string Username { get; private set; }
		public string Nickname { get; private set; }
		public bool IsMute { get; private set; }
		public bool IsDeaf { get; private set; }
		public IList<Role> Roles { get; private set; }

		public User(BestGirlClient client, ulong id, string username, Guild guild, string nickname, bool mute, bool deaf, IEnumerable<Role> roles)
		{
			Client = client;
			Id = id;
			Guild = guild;
			Username = username;
			Nickname = nickname;
			IsMute = mute;
			IsDeaf = deaf;
			Roles = roles == null ? new List<Role>() : roles.ToList();
		}
	}
}
