using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class UserGuild
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("icon")]
		public string Icon { get; set; }
		[JsonProperty("owner")]
		public bool Owner { get; set; }
		[JsonProperty("permissions")]
		public int Permissions { get; set; }
	}
}
