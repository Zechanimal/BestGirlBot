using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildMembersChunkEvent : EventMessage<GuildMembersChunkPayload>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.GuildMembersChunk;
			}
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
