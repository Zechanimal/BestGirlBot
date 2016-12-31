using Newtonsoft.Json;

namespace BestGirlBot.Discord.Gateway.Payloads
{
	public class IdentifyPayload
	{
		[JsonProperty("token")]
		public string Token { get; set; }
		[JsonProperty("properties")]
		public object Properties { get; set; }
		[JsonProperty("compress")]
		public bool Compress { get; set; }
		[JsonProperty("large_threshold")]
		public int LargeThreshold { get; set; }
		[JsonProperty("shards")]
		public int[] Shards { get; set; }
	}
}
