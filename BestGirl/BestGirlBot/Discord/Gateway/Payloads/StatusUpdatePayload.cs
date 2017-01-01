using Newtonsoft.Json;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Payloads
{
	public class StatusUpdatePayload
	{
		[JsonProperty("idle_since")]
		public int? IdleSince { get; set; }
		[JsonProperty("game")]
		public GameObject Game { get; set; }
	}
}
