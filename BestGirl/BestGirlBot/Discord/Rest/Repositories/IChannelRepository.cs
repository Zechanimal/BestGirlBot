using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Rest.Repositories
{
	public interface IChannelRepository
	{
		Channel GetChannel(ulong channelId);
		void ModifyChannel(ulong channelId, string name, int position);
		void ModifyChannel(ulong channelId, string name, int position, string topic);
		void ModifyChannel(ulong channelId, string name, int position, int bitrate, int userLimit);
		void DeleteChannel(ulong channelId);
		Message[] GetChannelMessages(ulong channelId);
		Message GetChannelMessage(ulong channelId, ulong messageId);
		void CreateChannelMessage(ulong channelId, string content);
		void EditMessage(ulong channelId, ulong messageId, string content);
		void DeleteMessage(ulong channelId, ulong messageId);
		void BulkDeleteMessages(ulong channelId, ulong[] messageIds);
	}
}
