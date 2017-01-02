using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DiscordGuild = BestGirlBot.Discord.Models.Guild;
using DiscordGuildChannel = BestGirlBot.Discord.Models.GuildChannel;
using DiscordRole = BestGirlBot.Discord.Models.Role;
using DiscordUser = BestGirlBot.Discord.Models.User;

namespace BestGirlBot.Client.Models
{
	public class Guild
	{
		private ConcurrentDictionary<ulong, Channel> _channels;
		private ConcurrentDictionary<ulong, Role> _roles;
		private ConcurrentDictionary<ulong, User> _users;

		public ulong Id { get; private set; }
		public string Name { get; private set; } = null;
		public bool Available { get; private set; } = false;
		public User Owner { get; private set; } = null;

		public Guild(ulong id)
		{
			Id = id;

			_channels = new ConcurrentDictionary<ulong, Channel>();
			_roles = new ConcurrentDictionary<ulong, Role>();
			_users = new ConcurrentDictionary<ulong, User>();
		}

		public IEnumerable<Channel> Channels
		{
			get
			{
				foreach (var channel in _channels)
				{
					yield return channel.Value;
				}
			}
		}

		public IEnumerable<Role> Roles
		{
			get
			{
				foreach (var role in _roles)
				{
					yield return role.Value;
				}
			}

		}

		public IEnumerable<User> Users
		{
			get
			{
				foreach (var user in _users)
				{
					yield return user.Value;
				}
			}
		}

		public void Create(DiscordGuild guildModel)
		{
			Name = guildModel.Name;
			Available = !guildModel.Unavailable;

			foreach (var role in guildModel.Roles)
			{
				_roles[role.Id] = new Role(role.Id, this, role.Name);
			}

			foreach (var member in guildModel.Members)
			{
				var memberRoles = member.Roles.Select(rid => _roles[rid]);
				_users[member.User.Id] = new User(member.User.Id, member.User.Username, this, member.Nick, member.Mute, member.Deaf, memberRoles);
			}

			foreach (var channel in guildModel.Channels)
			{
				Channel.Types type = channel.Type == DiscordGuildChannel.Types.Voice ? Channel.Types.Voice : Channel.Types.Text;
				_channels[channel.Id] = new Channel(channel.Id, this, channel.Name, type);
			}

			User owner;
			if (guildModel.OwnerId.HasValue && _users.TryGetValue(guildModel.OwnerId.Value, out owner))
			{
				Owner = owner;
			}
		}
	}
}
