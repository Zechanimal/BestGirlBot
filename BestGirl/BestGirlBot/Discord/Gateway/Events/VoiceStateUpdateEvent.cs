using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class VoiceStateUpdateEvent : EventMessage<VoiceState>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.VoiceStateUpdate;
			}
		}
	}
}
