using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class MessageDeleteBulkEvent : EventMessage<MessageDeleteBulkEvent, MessageDeleteBulkPayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.MessageDeleteBulk;
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
