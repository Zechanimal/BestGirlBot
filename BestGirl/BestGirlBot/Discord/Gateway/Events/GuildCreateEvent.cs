using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildCreateEvent : EventMessage<Guild>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.GuildCreate;
			}
		}
	}
}
