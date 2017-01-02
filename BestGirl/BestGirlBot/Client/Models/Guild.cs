using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DiscordGuild = BestGirlBot.Discord.Models.Guild;
using DiscordChannel = BestGirlBot.Discord.Models.Channel;
using DiscordRole = BestGirlBot.Discord.Models.Role;
using DiscordUser = BestGirlBot.Discord.Models.User;

namespace BestGirlBot.Client.Models
{
	public class Guild
	{
		public BestGirlClient Client { get; }

		public ulong Id { get; private set; }
		public string Name { get; private set; } = null;
		public bool Available { get; private set; } = false;
		public User Owner { get; private set; } = null;

		public IEnumerable<Channel> Channels => Client.GuildChannels.Where(c => c.Guild == this);
		public IEnumerable<User> Users => Client.Users.Where(u => u.Guild == this);
		public IEnumerable<Role> Roles => Client.Roles.Where(r => r.Guild == this);

		public Guild(BestGirlClient client, ulong id)
		{
			Client = client;
			Id = id;
		}

		public void Create(IEnumerable<User> users, IEnumerable<Channel> channels, IEnumerable<Role> roles, string name, bool available, User owner)
		{

		}
	}
}
