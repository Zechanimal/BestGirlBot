using System;
using BestGirlBot.Client.Models;

namespace BestGirlBot.Client.Events
{
	public class GuildEventArgs : EventArgs
	{
		public Guild Guild { get; set; }

		public GuildEventArgs(Guild guild)
		{
			Guild = guild;
		}
	}
}
