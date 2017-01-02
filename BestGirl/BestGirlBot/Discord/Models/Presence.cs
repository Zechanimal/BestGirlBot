using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class Presence
	{
		[JsonProperty("user")]
		public User User { get; set; }
		[JsonProperty("roles"), JsonConverter(typeof(GenericArrayConverter<SnowflakeJsonConverter>))]
		public ulong[] RoleIds { get; set; }
		[JsonProperty("game")]
		public GameObject Game { get; set; }
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("status")]
		public string Status { get; set; }

		public class StatusValues
		{
			public static readonly string Idle = "idle";
			public static readonly string Online = "online";
			public static readonly string Offline = "offline";
		}
	}
}
