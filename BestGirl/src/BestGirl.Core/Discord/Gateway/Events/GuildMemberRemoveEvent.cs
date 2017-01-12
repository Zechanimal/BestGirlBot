using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class GuildMemberRemoveEvent : EventMessage<GuildMemberRemoveEvent, GuildMemberRemovePayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildMemberRemove;
		}
	}

	public class GuildMemberRemovePayload
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("user")]
		public User User { get; set; }
	}
}
