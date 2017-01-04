using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BestGirlBot.Client.Models
{
	public class Guild
	{
		public BestGirlClient Client { get; }

		public ulong Id { get; private set; }
		public string Name { get; private set; } = null;
		public bool Available { get; private set; } = false;
		public Member Owner { get; private set; } = null;

		public IEnumerable<Channel> Channels => Client.GuildChannels.Where(c => c.Guild == this);
		public IEnumerable<User> Users => _members.Select(m => m.Value.User);
		public IEnumerable<Member> Members => _members.Select(m => m.Value);
		public IEnumerable<Role> Roles => Client.Roles.Where(r => r.Guild == this);
		public Member GetMember(ulong userId) => Members.First(m => m.User.Id == userId);

		private ConcurrentDictionary<ulong, Member> _members;

		public Guild(BestGirlClient client, ulong id)
		{
			Client = client;
			Id = id;

			_members = new ConcurrentDictionary<ulong, Member>();
		}

		public void Create(IEnumerable<Member> members, string name, bool available, Member owner)
		{
			foreach (var member in members)
			{
				_members[member.User.Id] = member;
			}
		}

		public void Update()
		{

		}
	}
}
