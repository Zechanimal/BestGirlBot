using Newtonsoft.Json;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Events
{
	public class GuildEmojisUpdateEvent : EventMessage<GuildEmojisUpdateEvent, GuildEmojisUpdatePayload>
	{
		public override GatewayEvent EventType()
		{
			return GatewayEvent.GuildEmojisUpdate;
		}
	}

	public class GuildEmojisUpdatePayload
	{
		[JsonProperty("guild_id")]
		public ulong GuildId { get; set; }
		[JsonProperty("emojis")]
		public Emoji[] Emojis { get; set; }
	}
}
