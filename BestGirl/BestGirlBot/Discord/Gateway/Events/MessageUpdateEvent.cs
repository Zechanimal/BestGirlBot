using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class MessageUpdateEvent : EventMessage<MessageUpdateEvent, Message>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.MessageUpdate;
		}
	}
}
