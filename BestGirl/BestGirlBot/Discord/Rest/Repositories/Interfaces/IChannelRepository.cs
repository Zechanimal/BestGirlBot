using System.Threading.Tasks;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Rest.Repositories.Interfaces
{
	public interface IChannelRepository
	{
		Task<Channel> GetChannelAsync(ulong channelId);
		Task<GuildChannel> ModifyChannelAsync(ulong channelId, string name, int position);
		Task<GuildTextChannel> ModifyChannelAsync(ulong channelId, string name, int position, string topic);
		Task<GuildVoiceChannel> ModifyChannelAsync(ulong channelId, string name, int position, int bitrate, int userLimit);
		Task<Channel> DeleteChannelAsync(ulong channelId);
		Task<Message[]> GetChannelMessagesAsync(ulong channelId);
		Task<Message> GetChannelMessageAsync(ulong channelId, ulong messageId);
		Task<Message> CreateMessageAsync(ulong channelId, string content);
		Task<Message> EditMessageAsync(ulong channelId, ulong messageId, string content);
		Task DeleteMessageAsync(ulong channelId, ulong messageId);
		Task BulkDeleteMessagesAsync(ulong channelId, ulong[] messageIds);
	}
}
