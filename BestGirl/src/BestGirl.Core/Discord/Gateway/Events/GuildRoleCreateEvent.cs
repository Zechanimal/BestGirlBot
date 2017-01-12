using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class GuildRoleCreateEvent : EventMessage<GuildRoleCreateEvent, GuildRoleCreatePayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildRoleCreate;
		}
	}

	public class GuildRoleCreatePayload
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("role")]
		public Role Role { get; set; }
	}
}
