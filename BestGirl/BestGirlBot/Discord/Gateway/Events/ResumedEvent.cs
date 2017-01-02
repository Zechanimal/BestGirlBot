using Newtonsoft.Json;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class ResumedEvent : EventMessage<ResumedPayload>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.Resumed;
			}
		}

	}

	public class ResumedPayload
	{
		[JsonProperty("_trace")]
		public string[] Trace { get; set; }
	}
}
