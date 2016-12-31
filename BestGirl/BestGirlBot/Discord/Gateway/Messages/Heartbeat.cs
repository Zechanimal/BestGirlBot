namespace BestGirlBot.Discord.Gateway.Messages
{
	public class Heartbeat : GatewayMessage
	{
		public Heartbeat(int previousSequence)
		{
			OpCode = GatewayOpCode.Heartbeat;
			Data = previousSequence;
		}
	}
}
