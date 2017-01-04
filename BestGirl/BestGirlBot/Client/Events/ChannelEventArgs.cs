using System;
using BestGirlBot.Client.Models;

namespace BestGirlBot.Client.Events
{
	public class ChannelEventArgs : EventArgs
	{
		public Channel Channel { get; set; }

		public ChannelEventArgs(Channel channel)
		{
			Channel = channel;
		}
	}
}
