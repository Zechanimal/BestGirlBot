using System;
using System.Threading.Tasks;
using BestGirl.Core.Osu.Models;

namespace BestGirl.Core.Osu.Repositories.Interfaces
{
	public interface IOsuRepository
	{
		Task<Beatmap[]> GetBeatmapSetAsync(DateTime since, string mapsetId, string user, UserType type = UserType.Any, GameMode mode = GameMode.All, bool converted = true, int limit = 500);
		Task<Beatmap[]> GetBeatmapsAsync(DateTime since, string beatmapId, string user, UserType type = UserType.Any, GameMode mode = GameMode.All, bool converted = true, int limit = 500);
		Task<User> GetUserAsync(string user, UserType type = UserType.Any, GameMode mode = GameMode.All, int eventDays = 1);
		Task<BeatmapScore[]> GetBeatmapScoresAsync(string beatmapId, GameMode mode = GameMode.Osu, int limit = 50);
		Task<BeatmapScore[]> GetBeatmapScoresAsync(string beatmapId, Mods mods, GameMode mode = GameMode.Osu, int limit = 50);
		Task<BeatmapScore[]> GetUserBeatmapScoresAsync(string beatmapId, string user, UserType type = UserType.Any, GameMode mode = GameMode.Osu, int limit = 50);
		Task<BeatmapScore[]> GetUserBeatmapScoresAsync(string beatmapId, Mods mods, string user, UserType type = UserType.Any, GameMode mode = GameMode.Osu, int limit = 50);
		Task<BeatmapScore[]> GetUserBestScoresAsync(string user, UserType type = UserType.Any, GameMode mode = GameMode.All, int limit = 10);
		Task<BeatmapScore[]> GetUserRecentScoresAsync(string user, UserType type = UserType.Any, GameMode mode = GameMode.All, int limit = 10);
	}
}
