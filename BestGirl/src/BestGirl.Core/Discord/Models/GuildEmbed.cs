using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Models
{
	public class GuildEmbed
	{
		[JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong ChannelId { get; set; }
		[JsonProperty("enabled")]
		public bool Enabled { get; set; }
	}
}
