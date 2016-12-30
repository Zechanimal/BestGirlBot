using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class Channel
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("is_private")]
		public bool IsPrivate { get; set; }
	}
}
