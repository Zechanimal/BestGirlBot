using System;
using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Models
{
	public class Message
	{
		[JsonProperty("id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
		[JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong ChannelId { get; set; }
		[JsonProperty("author")]
		public User Author { get; set; }
		[JsonProperty("content")]
		public string Content { get; set; }
		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; set; }
		[JsonProperty("edited_timestamp")]
		public DateTime? EditedTimestamp { get; set; }
		[JsonProperty("tts")]
		public bool TextToSpeech { get; set; }
		[JsonProperty("mention_everyone")]
		public bool MentionEveryone { get; set; }
		[JsonProperty("mentions")]
		public User[] Mentions { get; set; }
		[JsonProperty("mention_roles")]
		public Role[] MentionRoles { get; set; }
		//[JsonProperty("attachments")]
		//public Attachment[] Attachments { get; set; }
		//[JsonProperty("embeds")]
		//public Embed[] Embeds { get; set; }
		[JsonProperty("nonce"), JsonConverter(typeof(NullableSnowflakeJsonConverter))]
		public ulong? Nonce { get; set; }
		[JsonProperty("pinned")]
		public bool Pinned { get; set; }
		[JsonProperty("webhook_id")]
		public string WebhookId { get; set; }

		public static string MentionUser(ulong id)
		{
			return $"<@{id}>";
		}

		public static string MentionUserNick(ulong id)
		{
			return $"<@!{id}>";
		}

		public static string MentionChannel(ulong id)
		{
			return $"<#{id}>";
		}

		public static string MentionRole(ulong id)
		{
			return $"<@&{id}>";
		}

		public static string CustomEmoji(string name, ulong id)
		{
			return $"<:{name}:{id}>";
		}
	}
}
