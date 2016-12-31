using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BestGirlBot.Discord.Gateway
{
	public class GatewayMessage
	{
		[JsonProperty("op")]
		public GatewayOpCode OpCode { get; set; }
		[JsonProperty("d")]
		public object Data { get; set; }

		public T DataAs<T>()
		{
			return (Data as JObject).ToObject<T>();
		}
	}

	public class DispatchMessage : GatewayMessage
	{
		[JsonProperty("s")]
		public int Sequence { get; set; }
		[JsonProperty("t")]
		public string Type { get; set; }
	}
}
