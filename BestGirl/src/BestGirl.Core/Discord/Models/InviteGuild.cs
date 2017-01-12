using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Models
{
	public class InviteGuild
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("splash")]
		public string Splash { get; set; }
		[JsonProperty("icon")]
		public string Icon { get; set; }
	}
}
