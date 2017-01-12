using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Models
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
