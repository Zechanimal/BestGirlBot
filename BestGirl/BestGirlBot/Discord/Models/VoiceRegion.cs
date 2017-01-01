using Newtonsoft.Json;

namespace BestGirlBot.Discord.Models
{
	public class VoiceRegion
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("sample_hostname")]
		public string SampleHostname { get; set; }
		[JsonProperty("sample_port")]
		public int SamplePort { get; set; }
		[JsonProperty("vip")]
		public bool VIP { get; set; }
		[JsonProperty("optimal")]
		public bool Optimal { get; set; }
		[JsonProperty("deprecated")]
		public bool Deprecated { get; set; }
		[JsonProperty("custom")]
		public bool Custom { get; set; }
	}
}
