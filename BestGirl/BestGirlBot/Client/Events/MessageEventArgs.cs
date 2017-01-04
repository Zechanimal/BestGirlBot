using System;
using BestGirlBot.Client.Models;

namespace BestGirlBot.Client.Events
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
