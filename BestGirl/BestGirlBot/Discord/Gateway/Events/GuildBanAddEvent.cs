using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildBanAddEvent : EventMessage<GuildBanAddEvent, User>
	{
		public override GatewayEvent EventType()
		{
				return GatewayEvent.GuildBanAdd;
		}
	}
}
