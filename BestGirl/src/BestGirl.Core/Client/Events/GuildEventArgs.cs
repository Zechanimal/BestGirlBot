using System;
using BestGirl.Core.Client.Models;

namespace BestGirl.Core.Client.Events
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
