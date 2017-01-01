using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class GuildChannel : Channel
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

		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("position")]
		public int Position { get; set; }
		[JsonProperty("permission_overwrites")]
		public Overwrite[] PermissionOverwrites { get; set; }
	}
}
