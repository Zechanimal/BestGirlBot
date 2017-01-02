using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildUpdateEvent : EventMessage<GuildUpdateEvent, Guild>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildUpdate;
		}
	}
}
