using BestGirlBot.Discord.Gateway.Payloads;

namespace BestGirlBot.Discord.Gateway.Messages
{
	public class Identify : GatewayMessage
	{
		public Identify(string token, object properties, bool compress, int largeThreshold, int shardId, int shardCount)
		{
			OpCode = GatewayOpCode.Identify;
			Data = new IdentifyPayload
			{
				Token = token,
				Properties = properties == null ? new { } : properties,
				Compress = compress,
				LargeThreshold = largeThreshold,
				Shards = new[] { shardId, shardCount }
			};
		}
	}
}
