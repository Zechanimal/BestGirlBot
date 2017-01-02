using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildBanAddEvent : EventMessage<User>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.GuildBanAdd;
			}
		}
	}
}
