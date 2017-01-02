using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class Channel
	{
		public class Overwrite
		{
			[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
			public ulong Id { get; set; }
			[JsonProperty("type")]
			public string Type { get; set; }
			[JsonProperty("allow")]
			public Permissions Allow { get; set; }
			[JsonProperty("deny")]
			public Permissions Deny { get; set; }
		}

		public class Types
		{
			public static readonly string Text = "text";
			public static readonly string Voice = "voice";
		}

		// Channel
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("is_private")]
		public bool IsPrivate { get; set; }
		[JsonProperty("last_message_id"), JsonConverter(typeof(NullableSnowflakeJsonConverter))]
		public ulong? LastMessageId { get; set; }
		[JsonProperty("recipient")]
		public User Recipient { get; set; }

		// Guild Channel
		[JsonProperty("guild_id"), JsonConverter(typeof(NullableSnowflakeJsonConverter))]
		public ulong? GuildId { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("position")]
		public int Position { get; set; }
		[JsonProperty("permission_overwrites")]
		public Overwrite[] PermissionOverwrites { get; set; }

		// Voice Channel
		[JsonProperty("bitrate")]
		public int Bitrate { get; set; }
		[JsonProperty("user_limit")]
		public int UserLimit { get; set; }

		// Text Channel
		[JsonProperty("topic")]
		public string Topic { get; set; }
	}
}
