using System;
using System.Net.Http;
using System.Threading.Tasks;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Rest.Repositories.Interfaces
{
	public interface IGuildRepository
	{
		Task<Guild> GetGuildAsync(ulong guildId);
		Task<Guild> ModifyGuildAsync(ulong guildId, string name = null, string region = null, int? verificationLevel = null, int? defaultMessageNotifications = null,
			ulong? afkChannelId = null, int? afkTimeout = null, string icon = null, ulong? ownerId = null, string splash = null);
		Task<Guild> DeleteGuildAsync(ulong guildId);
		Task<Channel[]> GetChannelsAsync(ulong guildId);
		Task<Channel> CreateGuildTextChannelAsync(ulong guildId, string name, Permissions[] permissionOverwrites);
		Task<Channel> CreateGuildVoiceChannelAsync(ulong guildId, string name, int bitrate, int userLimit, Permissions[] permissionOverwrites);
		Task<Channel[]> ModifyChannelPositionsAsync(ulong guildId, Tuple<ulong, int>[] channelOrders);
		Task<GuildMember> GetGuildMemberAsync(ulong guildId, ulong userId);
		Task<GuildMember[]> ListGuildMembersAsync(ulong guildId, int limit = 1, int after = 0);
		Task<GuildMember> AddGuildMemberAsync(ulong guildId, ulong userId, string accessToken, string nick, Role[] roles, bool mute = false, bool deaf = false);
		Task<bool> ModifyGuildMemberAsync(ulong guildId, ulong userId, string nick = null, Role[] roles = null, bool? mute = null, bool? deaf = null, ulong? voiceChannelId = null);
		Task<bool> RemoveGuildMemberAsync(ulong guildId, ulong userId);
		Task<User[]> GetGuildBansAsync(ulong guildId);
		Task<bool> CreateGuildBanAsync(ulong guildId, ulong userId, int deleteMessageDays = 0);
		Task<bool> RemoveGuildBanAsync(ulong guildId, ulong userId);
		Task<Role[]> GetGuildRolesAsync(ulong guildId);
		Task<Role[]> ModifyGuildRolePositionsAsync(ulong guildId, Tuple<ulong, int>[] roleOrders);
		Task<Role> CreateGuildRoleAsync(ulong guildId);
		Task<Role> ModifyGuildRoleAsync(ulong guildId, ulong roleId, string name, Permissions permissions, int position, int color, bool hoist = true, bool mentionable = true);
		Task<Role> DeleteGuildRoleAsync(ulong guildId, ulong roleId);
		Task<int> GetGuildPruneCountAsync(ulong guildId, int days);
		Task<int> BeginGuildPruneAsync(ulong guildId, int days);
		Task<VoiceRegion[]> GetGuildVoiceRegionsAsync(ulong guildId);
		Task<InviteWithMetadata[]> GetGuildInvitesAsync(ulong guildId);
		Task<Integration[]> GetGuildIntegrationsAsync(ulong guildId);
		Task<bool> CreateGuildIntegrationAsync(ulong guildId, string type, ulong integrationId);
		Task<bool> ModifyGuildIntegrationAsync(ulong guildId, ulong integrationId, int expireBehavior, int expireGracePeriodSeconds, bool enableEmoticons);
		Task<bool> DeleteGuildIntegrationAsync(ulong guildId, ulong integrationId);
		Task<bool> SyncGuildIntegrationAsync(ulong guildId, ulong integrationId);
		Task<GuildEmbed> GetGuildEmbedAsync(ulong guildId);
		Task<GuildEmbed> ModifyGuildEmbedAsync(ulong guildId, bool? enabled = null, ulong? channelId = null);
	}
}
