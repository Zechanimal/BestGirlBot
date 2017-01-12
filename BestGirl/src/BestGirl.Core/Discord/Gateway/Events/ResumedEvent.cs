using Newtonsoft.Json;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class ResumedEvent : EventMessage<ResumedEvent, ResumedPayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.Resumed;
		}

	}

	public class ResumedPayload
	{
		[JsonProperty("_trace")]
		public string[] Trace { get; set; }
	}
}
