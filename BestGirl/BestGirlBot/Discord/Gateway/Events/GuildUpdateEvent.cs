using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildUpdateEvent : EventMessage<Guild>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.GuildUpdate;
			}
		}
	}
}
