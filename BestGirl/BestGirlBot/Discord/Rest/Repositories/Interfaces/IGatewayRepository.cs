using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Rest.Repositories.Interfaces
{
	public interface IGatewayRepository
	{
		GatewayObject GetGateway();
	}
}
