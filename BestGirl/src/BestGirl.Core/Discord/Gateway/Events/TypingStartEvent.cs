using System;
using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class TypingStartEvent : EventMessage<TypingStartEvent, TypingStartPayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.TypingStart;
		}
	}

	public class TypingStartPayload
	{
		[JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong ChannelId { get; set; }
		[JsonProperty("User_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong UserId { get; set; }
		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; set; }
	}
}
