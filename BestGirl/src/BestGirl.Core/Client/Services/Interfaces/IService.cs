using BestGirl.Core.Client.Events;
using BestGirl.Core.Client.Models;

namespace BestGirl.Core.Client.Services.Interfaces
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
