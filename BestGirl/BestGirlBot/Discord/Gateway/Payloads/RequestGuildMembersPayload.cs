using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Gateway.Payloads
{
	public class RequestGuildMembersPayload
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public string GuildId { get; set; }
		[JsonProperty("query")]
		public string Query { get; set; }
		[JsonProperty("limit")]
		public int Limit { get; set; }
	}
}
