﻿using Newtonsoft.Json.Linq;
using BestGirlBot.Extensions;

namespace BestGirlBot.Discord.Gateway
{
	public abstract class EventMessage<TEventMessage, TPayload> : GatewayMessage where TEventMessage : EventMessage<TEventMessage, TPayload>
	{
		public TPayload EventData() { return DataAs<TPayload>(); }
		public abstract GatewayEvent EventType();
		public string EventName() { return EventType().GetDescription(); }

		public static TEventMessage CreateEventMessage(GatewayMessage message)
		{
			return (JObject.FromObject(message)).ToObject<TEventMessage>();
		}
	}
}
