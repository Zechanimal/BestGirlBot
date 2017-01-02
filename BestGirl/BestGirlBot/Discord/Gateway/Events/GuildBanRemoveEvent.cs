using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildBanRemoveEvent : EventMessage<GuildBanRemoveEvent, User>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildBanRemove;
		}
	}
}
