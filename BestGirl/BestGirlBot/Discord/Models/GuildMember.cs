using System;
using Newtonsoft.Json;

namespace BestGirlBot.Discord.Models
{
	public class GuildMember
	{
		[JsonProperty("user")]
		public User User { get; set; }
		[JsonProperty("nick")]
		public string Nick { get; set; }
		[JsonProperty("roles")]
		public Role[] Roles { get; set; }
		[JsonProperty("joined_at")]
		public DateTime JoinedAt { get; set; }
		[JsonProperty("deaf")]
		public bool Deaf { get; set; }
		[JsonProperty("mute")]
		public bool Mute { get; set; }
	}
}
