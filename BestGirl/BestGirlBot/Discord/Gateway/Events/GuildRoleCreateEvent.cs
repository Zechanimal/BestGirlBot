using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildRoleCreateEvent : EventMessage<GuildRoleCreatePayload>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.GuildRoleCreate;
			}
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
