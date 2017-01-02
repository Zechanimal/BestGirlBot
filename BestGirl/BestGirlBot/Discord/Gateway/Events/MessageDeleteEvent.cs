using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class MessageDeleteEvent : EventMessage<MessageDeletePayload>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.MessageDelete;
			}
		}
	}

	public class MessageDeletePayload
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("message_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong Id { get; set; }
	}
}
