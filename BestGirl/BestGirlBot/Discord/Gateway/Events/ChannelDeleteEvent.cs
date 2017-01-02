using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class ChannelDeleteEvent : EventMessage<ChannelDeleteEvent, Channel>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.ChannelDelete;
		}
	}
}
