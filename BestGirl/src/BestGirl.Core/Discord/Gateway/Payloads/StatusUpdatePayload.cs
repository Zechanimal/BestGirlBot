using Newtonsoft.Json;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Payloads
{
	public class StatusUpdatePayload
	{
		[JsonProperty("idle_since")]
		public int? IdleSince { get; set; }
		[JsonProperty("game")]
		public GameObject Game { get; set; }
	}
}
