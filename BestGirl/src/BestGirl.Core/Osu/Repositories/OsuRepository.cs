using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BestGirl.Core.Osu.Models;
using BestGirl.Core.Osu.Repositories.Interfaces;

namespace BestGirl.Core.Osu.Repositories
{
	public class OsuRepository : IOsuRepository
	{
		private HttpClient Client { get; } = new HttpClient();
		private string ApiKey { get; set; }

		public OsuRepository(string apiKey)
		{
			ApiKey = apiKey;
			Client.BaseAddress = new Uri("https://osu.ppy.sh/api/");
		}

		private async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string> queryParams = null)
		{
			StringBuilder queryBuilder = new StringBuilder();
			queryBuilder.Append($"?k={ApiKey}");

			foreach (var param in queryParams)
			{
				queryBuilder.Append($"&{param.Key}={param.Value}");
			}

			var response = await Client.GetAsync(endpoint + queryBuilder.ToString());
			var body = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(body);
		}

		public async Task<Beatmap[]> GetBeatmapsAsync(DateTime since, string beatmapId, string user, UserType type, GameMode mode = GameMode.All, bool converted = true, int limit = 500)
		{
			Dictionary<string, string> queryParams = new Dictionary<string, string>();
			queryParams.Add("since", since.ToString("yyyy-MM-dd HH:mm:ss"));
			queryParams.Add("b", beatmapId);
			queryParams.Add("u", user);
			if (type != UserType.Any) queryParams.Add("type", ((int)type).ToString());
			if (mode != GameMode.All) queryParams.Add("m", ((int)mode).ToString());
			queryParams.Add("a", converted ? "1" : "0");
			queryParams.Add("limit", limit.ToString());

			return await GetAsync<Beatmap[]>("get_beatmaps", queryParams);
		}

		public async Task<BeatmapScore[]> GetBeatmapScoresAsync(string beatmapId, GameMode mode = GameMode.Osu, int limit = 50)
		{
			Dictionary<string, string> queryParams = new Dictionary<string, string>();
			queryParams.Add("b", beatmapId);
			queryParams.Add("m", ((int)mode).ToString());
			queryParams.Add("limit", limit.ToString());

			return await GetAsync<BeatmapScore[]>("get_scores", queryParams);
		}

		public Task<BeatmapScore[]> GetBeatmapScoresAsync(string beatmapId, Mods mods, GameMode mode = GameMode.Osu, int limit = 50)
		{
			Dictionary<string, string> queryParams = new Dictionary<string, string>();
			throw new NotImplementedException();
		}

		public Task<Beatmap[]> GetBeatmapSetAsync(DateTime since, string mapsetId, string user, UserType type = UserType.Any, GameMode mode = GameMode.All, bool converted = true, int limit = 500)
		{
			Dictionary<string, string> queryParams = new Dictionary<string, string>();
			throw new NotImplementedException();
		}

		public Task<User> GetUserAsync(string user, UserType type = UserType.Any, GameMode mode = GameMode.All, int eventDays = 1)
		{
			Dictionary<string, string> queryParams = new Dictionary<string, string>();
			throw new NotImplementedException();
		}

		public Task<BeatmapScore[]> GetUserBeatmapScoresAsync(string beatmapId, string user, UserType type = UserType.Any, GameMode mode = GameMode.Osu, int limit = 50)
		{
			Dictionary<string, string> queryParams = new Dictionary<string, string>();
			throw new NotImplementedException();
		}

		public Task<BeatmapScore[]> GetUserBeatmapScoresAsync(string beatmapId, Mods mods, string user, UserType type = UserType.Any, GameMode mode = GameMode.Osu, int limit = 50)
		{
			Dictionary<string, string> queryParams = new Dictionary<string, string>();
			throw new NotImplementedException();
		}

		public Task<BeatmapScore[]> GetUserBestScoresAsync(string user, UserType type = UserType.Any, GameMode mode = GameMode.All, int limit = 10)
		{
			Dictionary<string, string> queryParams = new Dictionary<string, string>();
			throw new NotImplementedException();
		}

		public Task<BeatmapScore[]> GetUserRecentScoresAsync(string user, UserType type = UserType.Any, GameMode mode = GameMode.All, int limit = 10)
		{
			Dictionary<string, string> queryParams = new Dictionary<string, string>();
			throw new NotImplementedException();
		}
	}
}
