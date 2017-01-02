using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class UserUpdateEvent : EventMessage<User>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.UserUpdate;
			}
		}
	}
}
