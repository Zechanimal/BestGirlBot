using Newtonsoft.Json;

namespace BestGirlBot.Discord.Models
{
	public class GameObject
	{
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
