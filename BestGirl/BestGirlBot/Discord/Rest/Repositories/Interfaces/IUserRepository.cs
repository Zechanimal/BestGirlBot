using System.Collections.Generic;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Rest.Repositories.Interfaces
{
	public interface IUserRepository
	{
		User GetCurrentUser();
		User GetUser(ulong userId);
		User ModifyCurrentUser(string username, string avatarData);
		UserGuild[] GetCurrentUserGuilds();
		void LeaveGuild(ulong guildId);
		DMChannel[] GetUserDMs();
		DMChannel CreateDM(ulong recipientId);
		DMChannel CreateGroupDM(string[] accessTokens, IDictionary<ulong, string> nicks);
		Connection[] GetUserConnections();
	}
}
