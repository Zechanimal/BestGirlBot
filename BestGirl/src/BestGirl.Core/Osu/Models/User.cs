using Newtonsoft.Json;

namespace BestGirl.Core.Osu.Models
{
	public class User
	{
		[JsonProperty("user_id")]
		public string Id { get; set; }
		[JsonProperty("username")]
		public string Username { get; set; }
		[JsonProperty("count300")]
		public string Count300 { get; set; }
		[JsonProperty("count100")]
		public string Count100 { get; set; }
		[JsonProperty("count50")]
		public string Count50 { get; set; }
		[JsonProperty("playcount")]
		public string PlayCount { get; set; }
		[JsonProperty("ranked_score")]
		public string RankedScore { get; set; }
		[JsonProperty("total_score")]
		public string TotalScore { get; set; }
		[JsonProperty("pp_rank")]
		public string Rank { get; set; }
		[JsonProperty("level")]
		public string Level { get; set; }
		[JsonProperty("pp_raw")]
		public string PP { get; set; }
		[JsonProperty("accuracy")]
		public string Accuracy { get; set; }
		[JsonProperty("count_rank_ss")]
		public string CountRankedSS { get; set; }
		[JsonProperty("count_rank_s")]
		public string CountRankedS { get; set; }
		[JsonProperty("count_rank_a")]
		public string CountRankedA { get; set; }
		[JsonProperty("country")]
		public string Country { get; set; }
		[JsonProperty("pp_country_rank")]
		public string CountryRank { get; set; }
		[JsonProperty("events")]
		public object events { get; set; }
	}
}
