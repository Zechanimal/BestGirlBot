using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class MessageDeleteBulkEvent : EventMessage<MessageDeleteBulkPayload>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.MessageDeleteBulk;
			}
		}
	}

	public class MessageDeleteBulkPayload
	{
		[JsonProperty("channel_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong ChannelId { get; set; }
		[JsonProperty("ids"), JsonConverter(typeof(GenericArrayConverter<SnowflakeJsonConverter>))]
		public ulong[] MessageIds { get; set; }
	}
}
