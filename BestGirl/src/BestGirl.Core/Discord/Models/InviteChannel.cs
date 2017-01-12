using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Models
{
	public class InviteChannel
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
