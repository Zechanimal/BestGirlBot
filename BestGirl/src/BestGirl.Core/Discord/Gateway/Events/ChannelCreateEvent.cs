using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class ChannelCreateEvent : EventMessage<ChannelCreateEvent, Channel>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.ChannelCreate;
		}
	}
}
