using Newtonsoft.Json;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class ReadyEvent : EventMessage<ReadyEvent, ReadyPayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.Ready;
		}
	}

	public class ReadyPayload
	{
		[JsonProperty("v")]
		public int ProtocolVersion { get; set; }
		[JsonProperty("user")]
		public User User { get; set; }
		[JsonProperty("private_channels")]
		public Channel[] PrivateChannels { get; set; }
		[JsonProperty("guilds")]
		public Guild[] Guilds { get; set; }
		[JsonProperty("session_id")]
		public string SessionId { get; set; }
		//[JsonProperty("presences")]
		// public Presence[] Presences { get; set; } // Not for bots
		//[JsonProperty("relationships")]
		// public User[] Relationships { get; set; } // Not for bots
		[JsonProperty("_trace")]
		public string[] Trace { get; set; }
	}
}
