using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Models
{
	public class DMChannel : Channel
	{
		[JsonProperty("recipient")]
		public User Recipient { get; set; }
		[JsonProperty("last_message_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public string LastMessageId { get; set; }
	}
}
