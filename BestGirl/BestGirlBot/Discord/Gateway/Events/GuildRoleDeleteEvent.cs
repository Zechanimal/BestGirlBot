﻿using Newtonsoft.Json;
using BestGirlBot.Discord.Converters;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class GuildRoleDeleteEvent : EventMessage<GuildRoleDeletePayload>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.GuildRoleDelete;
			}
		}
	}

	public class GuildRoleDeletePayload
	{
		[JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong GuildId { get; set; }
		[JsonProperty("role_id"), JsonConverter(typeof(SnowflakeJsonConverter))]
		public ulong RoleId { get; set; }
	}
}
