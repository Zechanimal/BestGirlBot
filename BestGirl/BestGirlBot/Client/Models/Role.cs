namespace BestGirlBot.Client.Models
{
	public class Role
	{
		public ulong Id { get; private set; }
		public Guild Guild { get; private set; }
		public string Name { get; private set; }

		public Role(ulong id, Guild guild, string name)
		{
			Id = id;
			Guild = guild;
			Name = name;
		}
	}
}
