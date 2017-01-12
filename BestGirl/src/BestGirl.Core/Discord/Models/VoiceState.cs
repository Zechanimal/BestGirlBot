using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Models
{
	public class VoiceState
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(NullableSnowflakeJsonConverter))]
		public ulong? GuildId { get; set; }
		[JsonProperty("channel_id"), JsonConverter(typeof(NullableSnowflakeJsonConverter))]
		public ulong? ChannelId { get; set; }
		[JsonProperty("user_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong UserId { get; set; }
		[JsonProperty("session_id")]
		public string SessionId { get; set; }
		[JsonProperty("deaf")]
		public bool? Deaf { get; set; }
		[JsonProperty("mute")]
		public bool? Mute { get; set; }
		[JsonProperty("self_deaf")]
		public bool? SelfDeaf { get; set; }
		[JsonProperty("self_mute")]
		public bool? SelfMute { get; set; }
		[JsonProperty("suppress")]
		public bool? Suppress { get; set; }
	}
}
