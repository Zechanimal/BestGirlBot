using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class UserUpdateEvent : EventMessage<UserUpdateEvent, User>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.UserUpdate;
		}
	}
}
