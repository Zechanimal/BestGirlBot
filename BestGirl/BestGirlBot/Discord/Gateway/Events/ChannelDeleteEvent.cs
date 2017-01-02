using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class ChannelDeleteEvent : EventMessage<Channel>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.ChannelDelete;
			}
		}
	}
}
