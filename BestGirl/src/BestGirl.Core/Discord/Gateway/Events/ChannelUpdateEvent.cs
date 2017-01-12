using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class ChannelUpdateEvent : EventMessage<ChannelUpdateEvent, Channel>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.ChannelUpdate;
		}
	}
}
