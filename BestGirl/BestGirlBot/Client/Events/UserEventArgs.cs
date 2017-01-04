using System;
using BestGirlBot.Client.Models;

namespace BestGirlBot.Client.Events
{
	public class UserEventArgs : EventArgs
	{
		public User User { get; set; }

		public UserEventArgs(User user)
		{
			User = user;
		}
	}
}
