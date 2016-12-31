using Newtonsoft.Json;

namespace BestGirlBot.Discord.Models
{
	public class GatewayObject
	{
		[JsonProperty("url")]
		public string Url { get; set; }
		[JsonProperty("shards")]
		public int Shards { get; set; }
	}
}
