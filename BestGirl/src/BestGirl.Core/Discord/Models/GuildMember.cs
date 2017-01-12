using System;
using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Models
{
	public class GuildMember
	{
		[JsonProperty("user")]
		public User User { get; set; }
		[JsonProperty("nick")]
		public string Nick { get; set; }
		[JsonProperty("roles"), JsonConverter(typeof(GenericArrayConverter<SnowflakeJsonConverter>))]
		public ulong[] RoleIds { get; set; }
		[JsonProperty("joined_at")]
		public DateTime JoinedAt { get; set; }
		[JsonProperty("deaf")]
		public bool Deaf { get; set; }
		[JsonProperty("mute")]
		public bool Mute { get; set; }
		[JsonProperty("guild_id"), JsonConverter(typeof(NullableSnowflakeJsonConverter))]
		public ulong? GuildId { get; set; }
	}
}
