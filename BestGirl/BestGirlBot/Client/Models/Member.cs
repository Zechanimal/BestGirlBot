using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestGirlBot.Client.Models
{
	public class Member
	{
		public BestGirlClient Client { get; }

		public User User { get; private set; }
		public Guild Guild { get; private set; }
		public string Nickname { get; private set; }
		public bool IsMute { get; private set; }
		public bool IsDeaf { get; private set; }
		public IEnumerable<Role> Roles { get; private set; }

		public Member(BestGirlClient client, User user, Guild guild, string nick, bool mute, bool deaf, IEnumerable<Role> roles)
		{
			Client = client;
			User = user;
			Guild = guild;
			Nickname = nick;
			IsMute = mute;
			IsDeaf = deaf;
			Roles = roles;
		}
	}
}
