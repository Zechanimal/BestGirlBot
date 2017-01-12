using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class MessageUpdateEvent : EventMessage<MessageUpdateEvent, Message>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.MessageUpdate;
		}
	}
}
