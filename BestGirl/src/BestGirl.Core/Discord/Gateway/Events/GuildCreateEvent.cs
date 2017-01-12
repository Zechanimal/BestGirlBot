using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class GuildCreateEvent : EventMessage<GuildCreateEvent, Guild>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildCreate;
		}
	}
}
