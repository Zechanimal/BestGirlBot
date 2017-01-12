using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class PresenceUpdateEvent : EventMessage<PresenceUpdateEvent, PresenceUpdatePayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.PresenceUpdate;
		}
	}

	public class PresenceUpdatePayload
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("roles"), JsonConverter(typeof(GenericArrayConverter<SnowflakeJsonConverter>))]
		public ulong[] RoleIds { get; set; }
		[JsonProperty("user")]
		public User User { get; set; }
		[JsonProperty("game")]
		public GameObject Game { get; set; }
		[JsonProperty("status")]
		public string Status { get; set; }
	}
}
