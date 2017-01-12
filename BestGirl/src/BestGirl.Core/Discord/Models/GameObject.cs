using Newtonsoft.Json;

namespace BestGirl.Core.Discord.Models
{
	public class GameObject
	{
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
