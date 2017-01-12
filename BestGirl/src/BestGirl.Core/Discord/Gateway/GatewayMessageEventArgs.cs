namespace BestGirl.Core.Discord.Gateway
{
	public class GatewayMessageEventArgs
	{
		public GatewayMessageEventArgs(GatewayMessage message)
		{
			Message = message;
		}

		public GatewayMessage Message { get; set; }
	}
}
