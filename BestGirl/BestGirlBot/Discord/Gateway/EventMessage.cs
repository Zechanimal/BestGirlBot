using BestGirlBot.Extensions;

namespace BestGirlBot.Discord.Gateway
{
	public abstract class EventMessage<TPayload> : GatewayMessage
	{
		TPayload EventData { get { return DataAs<TPayload>(); } }
		public abstract GatewayEvent EventType { get; }
		public string EventName { get { return EventType.GetDescription(); } }
	}
}
