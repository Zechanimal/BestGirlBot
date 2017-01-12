using System;
using BestGirl.Core.Client.Models;

namespace BestGirl.Core.Client.Events
{
	public class MessageEventArgs : EventArgs
	{
		public Message Message { get; set; }

		public MessageEventArgs(Message message)
		{
			Message = message;
		}
	}
}
