using System;
using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class TypingStartEvent : EventMessage<TypingStartPayload>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.TypingStart;
			}
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
