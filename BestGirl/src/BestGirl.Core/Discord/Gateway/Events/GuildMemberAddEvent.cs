using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class GuildMemberAddEvent : EventMessage<GuildMemberAddEvent, GuildMember>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildMemberAdd;
		}
	}
}
