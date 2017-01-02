using System;

namespace BestGirlBot.Client.Events
{
	public class HeartbeatEventArgs : EventArgs
	{
		public DateTime PreviousHeartbeatTime { get; set; }
		public DateTime HeartbeatTime { get; set; }
		public bool ServerHeartbeat { get; set; }

		public HeartbeatEventArgs(DateTime previous, DateTime now, bool server)
		{
			PreviousHeartbeatTime = previous;
			HeartbeatTime = now;
			ServerHeartbeat = server;
		}
	}
}
