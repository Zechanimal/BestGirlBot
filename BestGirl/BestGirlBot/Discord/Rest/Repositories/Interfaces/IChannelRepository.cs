﻿using System.Threading.Tasks;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Rest.Repositories.Interfaces
{
	public interface IChannelRepository
	{
		Task<Channel> GetChannelAsync(ulong channelId);
		Task<Channel> ModifyChannelAsync(ulong channelId, string name, int position);
		Task<Channel> ModifyTextChannelAsync(ulong channelId, string name, int position, string topic);
		Task<Channel> ModifyVoiceChannelAsync(ulong channelId, string name, int position, int bitrate, int userLimit);
		Task<Channel> DeleteChannelAsync(ulong channelId);
		Task<Message[]> GetChannelMessagesAroundAsync(ulong channelId, ulong messageId, int limit);
		Task<Message[]> GetChannelMessagesBeforeAsync(ulong channelId, ulong messageId, int limit);
		Task<Message[]> GetChannelMessagesAfterAsync(ulong channelId, ulong messageId, int limit);
		Task<Message> GetChannelMessageAsync(ulong channelId, ulong messageId);
		Task<Message> CreateMessageAsync(ulong channelId, string content);
		Task<Message> EditMessageAsync(ulong channelId, ulong messageId, string content);
		Task<bool> DeleteMessageAsync(ulong channelId, ulong messageId);
		Task<bool> BulkDeleteMessagesAsync(ulong channelId, ulong[] messageIds);
	}
}
