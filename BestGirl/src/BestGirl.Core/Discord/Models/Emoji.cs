using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Models
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
