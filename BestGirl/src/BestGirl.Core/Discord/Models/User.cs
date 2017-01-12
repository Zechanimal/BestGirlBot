using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Models
{
	public class User
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("guild_id"), JsonConverter(typeof(NullableSnowflakeJsonConverter))]
		public ulong? GuildId { get; set; }
		[JsonProperty("username")]
		public string Username { get; set; }
		[JsonProperty("discriminator")]
		public string Discriminator { get; set; }
		[JsonProperty("avatar")]
		public string Avatar { get; set; }
		[JsonProperty("bot")]
		public bool? Bot { get; set; }
	}

	public class ExtendedUser : User
	{
		[JsonProperty("mfa_enabled")]
		public bool? MfaEnabled { get; set; }
		[JsonProperty("verified")]
		public bool? Verified { get; set; }
		[JsonProperty("email")]
		public string Email { get; set; }
	}
}
