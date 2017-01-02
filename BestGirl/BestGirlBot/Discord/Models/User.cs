using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class User
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
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
