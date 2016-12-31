using Newtonsoft.Json;

namespace BestGirlBot.Discord.Gateway.Payloads
{
	public class HelloPayload
	{
		[JsonProperty("heartbeat_interval")]
		public int HeartbeatInterval { get; set; }
		[JsonProperty("_trace")]
		public string[] Trace { get; set; }
	}
}
