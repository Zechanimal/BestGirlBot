using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class VoiceServerUpdateEvent : EventMessage<VoiceServerUpdatePayload>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.VoiceServerUpdate;
			}
		}
	}

	public class VoiceServerUpdatePayload
	{
		[JsonProperty("token")]
		public string Token { get; set; }
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("endpoint")]
		public string Endpoint { get; set; }
	}
}
