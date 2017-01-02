﻿using Newtonsoft.Json;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildEmojisUpdateEvent : EventMessage<GuildEmojisUpdatePayload>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.GuildEmojisUpdate;
			}
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
