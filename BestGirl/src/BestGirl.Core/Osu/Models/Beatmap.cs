using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using BestGirl.Core.Extensions;

namespace BestGirl.Core.Osu.Models
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Beatmap
	{
		[JsonProperty("approved")]
		private string _approved { get; set; }
		[JsonProperty("approved_date")]
		private string _approvedDate { get; set; }
		[JsonProperty("last_update")]
		private string _lastUpdate { get; set; }
		[JsonProperty("artist")]
		public string Artist { get; set; }
		[JsonProperty("beatmap_id")]
		public string BeatmapId { get; set; }
		[JsonProperty("beatmapset_id")]
		public string BeatmapSetId { get; set; }
		[JsonProperty("bpm")]
		public string Bpm { get; set; }
		[JsonProperty("creator")]
		public string Creator { get; set; }
		[JsonProperty("difficultyrating")]
		public string StarRating { get; set; }
		[JsonProperty("diff_size")]
		public string CircleSize { get; set; }
		[JsonProperty("diff_overall")]
		public string OverallDifficulty { get; set; }
		[JsonProperty("diff_approach")]
		public string ApproachRate { get; set; }
		[JsonProperty("diff_drain")]
		public string Drain { get; set; }
		[JsonProperty("hit_length")]
		public string HitLength { get; set; }
		[JsonProperty("source")]
		public string Source { get; set; }
		[JsonProperty("genre_id")]
		private string _genreId { get; set; }
		[JsonProperty("language_id")]
		private string _languageId { get; set; }
		[JsonProperty("title")]
		public string Title { get; set; }
		[JsonProperty("total_length")]
		public string TotalLength { get; set; }
		[JsonProperty("version")]
		public string Version { get; set; }
		[JsonProperty("file_md5")]
		public string FileMd5 { get; set; }
		[JsonProperty("mode")]
		public string Mode { get; set; }
		[JsonProperty("tags")]
		private string _tags { get; set; }
		[JsonProperty("favourite_count")]
		public string FavoriteCount { get; set; }
		[JsonProperty("playcount")]
		public string PlayCount { get; set; }
		[JsonProperty("passcount")]
		public string PassCount { get; set; }

		public ApprovalStatus Approved
		{
			get { return _approved.ToEnumFromId<ApprovalStatus>(); }
		}

		public DateTime ApprovedDate
		{
			get { return DateTime.Parse(_approvedDate); }
		}

		public DateTime LastUpdate
		{
			get { return DateTime.Parse(_lastUpdate); }
		}

		public Genre Genre
		{
			get { return _genreId.ToEnumFromId<Genre>(); }
		}

		public Language Language
		{
			get { return _languageId.ToEnumFromId<Language>(); }
		}

		public List<string> Tags
		{
			get { return _tags.Split(' ').ToList(); }
		}
	}
}
