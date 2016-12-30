using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class Emoji
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("roles")]
		public Role[] Roles { get; set; }
		[JsonProperty("require_colons")]
		public bool RequireColons { get; set; }
		[JsonProperty("managed")]
		public bool Managed { get; set; }
	}
}
