using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class GuildUpdateEvent : EventMessage<GuildUpdateEvent, Guild>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildUpdate;
		}
	}
}
