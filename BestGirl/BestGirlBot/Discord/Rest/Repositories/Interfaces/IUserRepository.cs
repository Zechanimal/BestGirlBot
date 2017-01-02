using System.Collections.Generic;
using System.Threading.Tasks;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Rest.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<User> GetCurrentUserAsync();
		Task<User> GetUserAsync(ulong userId);
		Task<User> ModifyCurrentUserAsync(string username, string avatarData);
		Task<UserGuild[]> GetCurrentUserGuildsAsync();
		Task<bool> LeaveGuildAsync(ulong guildId);
		Task<DMChannel[]> GetUserDmsAsync();
		Task<DMChannel> CreateDmAsync(ulong recipientId);
		Task<DMChannel> CreateGroupDmAsync(string[] accessTokens, IDictionary<ulong, string> nicks);
		Task<Connection[]> GetUserConnectionsAsync();
	}
}
