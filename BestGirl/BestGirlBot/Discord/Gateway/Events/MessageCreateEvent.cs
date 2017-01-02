using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class MessageCreateEvent : EventMessage<MessageCreateEvent, Message>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.MessageCreate;
		}
	}
}
