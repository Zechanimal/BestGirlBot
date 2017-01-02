using System;
using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class Guild
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("icon")]
		public string Icon { get; set; }
		[JsonProperty("splash")]
		public string Splash { get; set; }
		[JsonProperty("owner_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong OwnerId { get; set; }
		[JsonProperty("region")]
		public string Region { get; set; }
		[JsonProperty("afk_channel_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong AfkChannelId { get; set; }
		[JsonProperty("afk_timeout")]
		public int AfkTimeout { get; set; }
		[JsonProperty("embed_enabled")]
		public bool EmbedEnabled { get; set; }
		[JsonProperty("embed_channel_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong EmbedChannelId { get; set; }
		[JsonProperty("verification_level")]
		public int VerificationLevel { get; set; }
		[JsonProperty("default_message_notifications")]
		public int DefaultMessageNotifications { get; set; }
		[JsonProperty("roles")]
		public Role[] Roles { get; set; }
		[JsonProperty("emojis")]
		public Emoji[] Emojis { get; set; }
		[JsonProperty("mfa_level")]
		public int MfaLevel { get; set; }
		[JsonProperty("member_count")]
		public int MemberCount { get; set; }
		[JsonProperty("joined_at")]
		public DateTime JoinedAt { get; set; }
		[JsonProperty("large")]
		public bool Large { get; set; }
		[JsonProperty("unavailable")]
		public bool Unavailable { get; set; }
		[JsonProperty("members")]
		public GuildMember[] Members { get; set; }
		[JsonProperty("channels")]
		public GuildChannel Channels { get; set; }
		[JsonProperty("presences")]
		public Presence[] Presences { get; set; }
		[JsonProperty("voice_states")]
		public VoiceState[] VoiceStates { get; set; }
	}
}
