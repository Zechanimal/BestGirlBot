using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class Role
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("color")]
		public int Color { get; set; }
		[JsonProperty("hoist")]
		public bool Hoist { get; set; }
		[JsonProperty("position")]
		public int Position { get; set; }
		[JsonProperty("permissions")]
		public Permissions Permissions { get; set; }
		[JsonProperty("managed")]
		public bool Managed { get; set; }
		[JsonProperty("mentionable")]
		public bool Mentionable { get; set; }
	}
}
