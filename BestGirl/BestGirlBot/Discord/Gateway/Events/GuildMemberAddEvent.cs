using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildMemberAddEvent : EventMessage<GuildMemberAddEvent, GuildMember>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildMemberAdd;
		}
	}
}
