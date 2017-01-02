using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class ChannelCreateEvent : EventMessage<Channel>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.ChannelCreate;
			}
		}
	}
}
