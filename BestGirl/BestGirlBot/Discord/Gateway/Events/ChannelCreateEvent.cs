using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class ChannelCreateEvent : EventMessage<ChannelCreateEvent, Channel>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.ChannelCreate;
		}
	}
}
