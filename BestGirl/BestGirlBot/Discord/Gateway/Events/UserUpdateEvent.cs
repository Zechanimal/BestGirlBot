using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class UserUpdateEvent : EventMessage<UserUpdateEvent, User>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.UserUpdate;
		}
	}
}
