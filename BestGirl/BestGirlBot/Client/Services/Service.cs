using System;
using BestGirlBot.Client.Events;
using BestGirlBot.Client.Services.Interfaces;

namespace BestGirlBot.Client.Services
{
	public abstract class Service : IService
	{
		public BestGirlClient Client { get; private set; }

		public abstract string Name { get; }
		public abstract string Description { get; }

		public void Start(BestGirlClient client)
		{
			Client = client;

			Client.ChannelCreate += OnChannelCreate;
			Client.ChannelDelete += OnChannelDelete;
			Client.ChannelUpdate += OnChannelUpdate;
			Client.GuildCreate += OnGuildCreate;
			Client.GuildDelete += OnGuildDelete;
			Client.GuildMemberAdd += OnGuildMemberAdd;
			Client.GuildMemberRemove += OnGuildMemberRemove;
			Client.GuildMemberUpdate += OnGuildMemberUpdate;
			Client.GuildUpdate += OnGuildUpdate;
			Client.Heartbeat += OnHeartbeat;
			Client.MessageCreate += OnMessageCreate;
			Client.MessageDelete += OnMessageDelete;
			Client.MessageUpdate += OnMessageUpdate;

			Initialize();
		}

		protected abstract void Initialize();

		protected abstract void OnChannelCreate(object sender, ChannelEventArgs e);
		protected abstract void OnChannelDelete(object sender, ChannelEventArgs e);
		protected abstract void OnChannelUpdate(object sender, ChannelEventArgs e);
		protected abstract void OnGuildCreate(object sender, GuildEventArgs e);
		protected abstract void OnGuildDelete(object sender, GuildEventArgs e);
		protected abstract void OnGuildMemberAdd(object sender, GuildEventArgs e);
		protected abstract void OnGuildMemberRemove(object sender, GuildEventArgs e);
		protected abstract void OnGuildMemberUpdate(object sender, GuildEventArgs e);
		protected abstract void OnGuildUpdate(object sender, GuildEventArgs e);
		protected abstract void OnHeartbeat(object sender, HeartbeatEventArgs e);
		protected abstract void OnMessageCreate(object sender, MessageEventArgs e);
		protected abstract void OnMessageDelete(object sender, MessageEventArgs e);
		protected abstract void OnMessageUpdate(object sender, MessageEventArgs e);
		protected abstract void OnUserUpdate(object sender, UserEventArgs e);
	}
}
