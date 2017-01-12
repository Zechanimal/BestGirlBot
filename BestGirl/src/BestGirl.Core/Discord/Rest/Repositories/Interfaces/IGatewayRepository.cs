using System.Threading.Tasks;
using BestGirl.Core.Discord.Models;

namespace BestGirl.Core.Discord.Rest.Repositories.Interfaces
{
	public interface IGatewayRepository
	{
		Task<GatewayObject> GetGatewayAsync();
	}
}
