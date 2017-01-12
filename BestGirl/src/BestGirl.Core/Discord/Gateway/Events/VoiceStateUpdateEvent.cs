using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class VoiceStateUpdateEvent : EventMessage<VoiceStateUpdateEvent, VoiceState>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.VoiceStateUpdate;
		}
	}
}
