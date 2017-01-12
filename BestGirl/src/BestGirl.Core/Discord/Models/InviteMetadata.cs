using System;
using Newtonsoft.Json;

namespace BestGirl.Core.Discord.Models
{
	public class InviteWithMetadata : Invite
	{
		[JsonProperty("inviter")]
		public User Inviter { get; set; }
		[JsonProperty("uses")]
		public int Uses { get; set; }
		[JsonProperty("max_uses")]
		public int MaxUses { get; set; }
		[JsonProperty("max_age")]
		public int MaxAgeSeconds { get; set; }
		[JsonProperty("temporary")]
		public bool IsTemporary { get; set; }
		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonProperty("revoked")]
		public bool IsRevoked { get; set; }
	}
}
