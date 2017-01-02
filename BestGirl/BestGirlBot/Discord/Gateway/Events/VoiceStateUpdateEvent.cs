using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class VoiceStateUpdateEvent : EventMessage<VoiceStateUpdateEvent, VoiceState>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.VoiceStateUpdate;
		}
	}
}
