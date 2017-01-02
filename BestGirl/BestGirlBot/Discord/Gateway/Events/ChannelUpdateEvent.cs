using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class ChannelUpdateEvent : EventMessage<ChannelUpdateEvent, Channel>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.ChannelUpdate;
		}
	}
}
