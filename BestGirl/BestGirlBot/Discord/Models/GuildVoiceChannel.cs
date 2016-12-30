using Newtonsoft.Json;

namespace BestGirlBot.Discord.Models
{
	public class GuildVoiceChannel : GuildChannel
	{
		[JsonProperty("bitrate")]
		public int Bitrate { get; set; }
		[JsonProperty("user_limit")]
		public int UserLimit { get; set; }
	}
}
