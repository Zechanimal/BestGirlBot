﻿using BestGirlBot.Discord.Gateway.Payloads;

namespace BestGirlBot.Discord.Gateway.Messages
{
	public class StatusUpdate : GatewayMessage
	{
		public StatusUpdate(int? idleSince, string gameName)
		{
			OpCode = GatewayOpCode.StatusUpdate;
			Data = new StatusUpdatePayload
			{
				IdleSince = idleSince,
				Game = new StatusUpdatePayload.GameObject
				{
					Name = gameName
				}
			};
		}
	}
}
