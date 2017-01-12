using Newtonsoft.Json;

namespace BestGirl.Core.Discord.Models
{
	public class Invite
	{
		[JsonProperty("code")]
		public string Code { get; set; }
		[JsonProperty("guild")]
		public InviteGuild Guild { get; set; }
		[JsonProperty("channel")]
		public InviteChannel Channel { get; set; }
	}
}
