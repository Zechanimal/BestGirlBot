using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildMemberAddEvent : EventMessage<GuildMember>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.GuildMemberAdd;
			}
		}
	}
}
