using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Models
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
