using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildDeleteEvent : EventMessage<GuildDeleteEvent, GuildDeletePayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildDelete;
		}
	}

	public class GuildDeletePayload
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("unavailable")]
		public bool Unavailable { get; set; }
	}
}
