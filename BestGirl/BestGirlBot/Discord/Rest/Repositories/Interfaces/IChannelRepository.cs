using System.Threading.Tasks;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Rest.Repositories.Interfaces
{
	public interface IChannelRepository
	{
		Task<Channel> GetChannelAsync(ulong channelId);
		Task ModifyChannelAsync(ulong channelId, string name, int position);
		Task ModifyChannelAsync(ulong channelId, string name, int position, string topic);
		Task ModifyChannelAsync(ulong channelId, string name, int position, int bitrate, int userLimit);
		Task DeleteChannelAsync(ulong channelId);
		Task<Message[]> GetChannelMessagesAsync(ulong channelId);
		Task<Message> GetChannelMessageAsync(ulong channelId, ulong messageId);
		Task CreateChannelMessageAsync(ulong channelId, string content);
		Task EditMessageAsync(ulong channelId, ulong messageId, string content);
		Task DeleteMessageAsync(ulong channelId, ulong messageId);
		Task BulkDeleteMessagesAsync(ulong channelId, ulong[] messageIds);
	}
}
