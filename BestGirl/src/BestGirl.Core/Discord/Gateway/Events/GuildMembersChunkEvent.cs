using Newtonsoft.Json;
using BestGirl.Core.Discord.Converters;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class GuildMembersChunkEvent : EventMessage<GuildMembersChunkEvent, GuildMembersChunkPayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildMembersChunk;
		}
	}

	public class GuildMembersChunkPayload
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("members")]
		public GuildMember[] Members { get; set; }
	}
}
