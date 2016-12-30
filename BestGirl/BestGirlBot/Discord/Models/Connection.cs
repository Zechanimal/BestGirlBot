using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class Connection
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("revoked")]
		public bool Revoked { get; set; }
		[JsonProperty("integrations")]
		public Integration[] Integrations { get; set; }
	}
}
