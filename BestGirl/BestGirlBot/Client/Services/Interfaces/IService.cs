using BestGirlBot.Client.Events;
using BestGirlBot.Client.Models;

namespace BestGirlBot.Client.Services.Interfaces
{
	public interface IService
	{
		BestGirlClient Client { get; }

		string Name { get; }
		string Description { get; }

		void Start(BestGirlClient client);

		void AddCommand(ICommand command);
	}
}
