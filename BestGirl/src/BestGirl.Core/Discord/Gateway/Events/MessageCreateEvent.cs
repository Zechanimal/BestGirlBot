using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class MessageCreateEvent : EventMessage<MessageCreateEvent, Message>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.MessageCreate;
		}
	}
}
