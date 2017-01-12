using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Gateway.Payloads
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
