using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class GuildTextChannel : GuildChannel
	{
		[JsonProperty("last_message_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public string LastMessageId { get; set; }
		[JsonProperty("topic")]
		public string Topic { get; set; }
	}
}
