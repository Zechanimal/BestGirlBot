using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class GuildBanAddEvent : EventMessage<GuildBanAddEvent, User>
	{
		public override GatewayEvent EventType()
		{
				return GatewayEvent.GuildBanAdd;
		}
	}
}
