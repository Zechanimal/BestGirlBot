using System.Threading.Tasks;
using BestGirlBot.Discord.Models;

namespace BestGirlBot.Discord.Rest.Repositories.Interfaces
{
	public interface IGatewayRepository
	{
		Task<GatewayObject> GetGatewayAsync();
	}
}
