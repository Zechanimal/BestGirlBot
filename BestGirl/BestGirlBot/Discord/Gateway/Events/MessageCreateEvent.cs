using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class MessageCreateEvent : EventMessage<Message>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.MessageCreate;
			}
		}
	}
}
