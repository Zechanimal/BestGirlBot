using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
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
