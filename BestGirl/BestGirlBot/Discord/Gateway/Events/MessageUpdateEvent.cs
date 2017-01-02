using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class MessageUpdateEvent : EventMessage<Message>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.MessageUpdate;
			}
		}
	}
}
