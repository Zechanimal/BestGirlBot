using System;
using Newtonsoft.Json;
using BestGirl.Core.Extensions;

namespace BestGirl.Core.Osu.Models
{
	[JsonObject(MemberSerialization.OptIn)]
	public class BeatmapScore
	{
		[JsonProperty("beatmap_id")]
		public string BeatmapId { get; set; }
		[JsonProperty("score")]
		public string Score { get; set; }
		[JsonProperty("username")]
		public string Username { get; set; }
		[JsonProperty("count300")]
		public string Count300 { get; set; }
		[JsonProperty("count100")]
		public string Count100 { get; set; }
		[JsonProperty("count50")]
		public string Count50 { get; set; }
		[JsonProperty("countmiss")]
		public string CountMiss { get; set; }
		[JsonProperty("maxcombo")]
		public string MaxCombo { get; set; }
		[JsonProperty("countkatu")]
		public string CountKatu { get; set; }
		[JsonProperty("countgeki")]
		public string CountGeki { get; set; }
		[JsonProperty("perfect")]
		private string _perfect { get; set; }
		[JsonProperty("enabled_mods")]
		private string _enabledMods { get; set; }
		[JsonProperty("user_id")]
		public string UserId { get; set; }
		[JsonProperty("date")]
		private string _date { get; set; }
		[JsonProperty("rank")]
		public string Rank { get; set; }
		[JsonProperty("pp")]
		public string PP { get; set; }

		public bool Perfect { get { return _perfect == "1"; } }
		public Mods Mods { get { return _enabledMods.ToEnumFromId<Mods>(); } }
		public DateTime Date { get { return DateTime.Parse(_date); } }
	}
}
