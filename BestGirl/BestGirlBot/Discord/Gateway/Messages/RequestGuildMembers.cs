using BestGirlBot.Discord.Gateway.Payloads;

namespace BestGirlBot.Discord.Gateway.Messages
{
	public class RequestGuildMembers : GatewayMessage
	{
		public RequestGuildMembers(string guildId, string query, int limit)
		{
			OpCode = GatewayOpCode.RequestGuildMembers;
			Data = new RequestGuildMembersPayload
			{
				GuildId = guildId,
				Query = query,
				Limit = limit
			};
		}
	}
}
