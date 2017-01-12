using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class ChannelDeleteEvent : EventMessage<ChannelDeleteEvent, Channel>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.ChannelDelete;
		}
	}
}
