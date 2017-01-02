using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildCreateEvent : EventMessage<GuildCreateEvent, Guild>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildCreate;
		}
	}
}
