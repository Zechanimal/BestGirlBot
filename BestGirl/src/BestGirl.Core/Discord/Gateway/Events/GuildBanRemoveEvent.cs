using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class GuildBanRemoveEvent : EventMessage<GuildBanRemoveEvent, User>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildBanRemove;
		}
	}
}
