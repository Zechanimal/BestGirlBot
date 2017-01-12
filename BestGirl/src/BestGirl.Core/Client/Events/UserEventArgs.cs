using System;
using BestGirl.Core.Client.Models;

namespace BestGirl.Core.Client.Events
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
