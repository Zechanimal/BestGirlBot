using Newtonsoft.Json;

namespace BestGirlBot.Discord.Gateway.Payloads
{
	public class StatusUpdatePayload
	{
		public class GameObject
		{
			[JsonProperty("name")]
			public string Name { get; set; }
		}

		[JsonProperty("idle_since")]
		public int? IdleSince { get; set; }
		[JsonProperty("game")]
		public GameObject Game { get; set; }
	}
}
