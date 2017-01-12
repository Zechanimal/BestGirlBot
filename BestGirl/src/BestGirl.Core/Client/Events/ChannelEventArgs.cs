using System;
using BestGirl.Core.Client.Models;

namespace BestGirl.Core.Client.Events
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
