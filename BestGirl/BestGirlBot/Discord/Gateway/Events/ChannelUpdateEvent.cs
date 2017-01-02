using System;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Gateway.Events
{
	public class ChannelUpdateEvent : EventMessage<Channel>
	{
		public override GatewayEvent EventType
		{
			get
			{
				return GatewayEvent.ChannelUpdate;
			}
		}
	}
}
