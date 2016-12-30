using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class Integration
	{
		public class IntegrationAccount
		{
			[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
			public ulong Id { get; set; }
			[JsonProperty("username")]
			public string Name { get; set; }
		}

		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("enabled")]
		public bool Enabled { get; set; }
		[JsonProperty("syncing")]
		public bool Syncing { get; set; }
		[JsonProperty("role_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong RoleId { get; set; }
		[JsonProperty("expire_behavior")]
		public int ExpireBehavior { get; set; }
		[JsonProperty("expire_grace_period")]
		public int ExpireGracePeriod { get; set; }
		[JsonProperty("user")]
		public User User { get; set; }
		[JsonProperty("account")]
		public IntegrationAccount Account { get; set; }
	}
}
