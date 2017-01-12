using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class GuildMemberUpdateEvent : EventMessage<GuildMemberUpdateEvent, GuildMemberUpdatePayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildMemberUpdate;
		}
	}

	public class GuildMemberUpdatePayload
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("user")]
		public User User { get; set; }
		[JsonProperty("roles"), JsonConverter(typeof(GenericArrayConverter<SnowflakeJsonConverter>))]
		public ulong[] RoleIds { get; set; }
		[JsonProperty("nick")]
		public string Nick { get; set; }
	}
}
