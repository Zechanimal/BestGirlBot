using BestGirl.Core.Discord.Gateway.Payloads;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Gateway.Messages
{
	public class StatusUpdate : GatewayMessage
	{
		public StatusUpdate(int? idleSince, string gameName)
		{
			OpCode = GatewayOpCode.StatusUpdate;
			Data = new StatusUpdatePayload
			{
				IdleSince = idleSince,
				Game = new GameObject
				{
					Name = gameName
				}
			};
		}
	}
}
