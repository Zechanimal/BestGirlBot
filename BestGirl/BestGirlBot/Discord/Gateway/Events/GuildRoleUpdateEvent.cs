using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildRoleUpdateEvent : EventMessage<GuildRoleUpdateEvent, GuildRoleUpdatePayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildRoleUpdate;
		}
	}

	public class GuildRoleUpdatePayload
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("role")]
		public Role Role { get; set; }
	}
}
