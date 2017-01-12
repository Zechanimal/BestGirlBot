using BestGirl.Core.Discord.Gateway.Payloads;

namespace BestGirl.Core.Discord.Gateway.Messages
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
