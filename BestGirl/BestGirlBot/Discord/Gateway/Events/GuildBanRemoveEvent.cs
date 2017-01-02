using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildBanRemoveEvent : EventMessage<User>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.GuildBanRemove;
			}
		}
	}
}
