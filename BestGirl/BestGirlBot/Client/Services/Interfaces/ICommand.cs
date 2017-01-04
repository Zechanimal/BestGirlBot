using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestGirlBot.Client.Models;

namespace BestGirlBot.Client.Services.Interfaces
{
	public interface ICommand
	{
		string Name { get; }
		string Description { get; }

		CommandConvention CallingConvention { get; }
		IEnumerable<string> CommandTriggers { get; }

		bool CanHandleMessage(BestGirlClient client, Message message);
		Task HandleMessage(BestGirlClient cilent, Message message);
	}

	[Flags]
	public enum CommandConvention
	{
		BeginningPhrase,
		BeginningBotUserMention,
		Regex,
		CaseInvariant
	}
}
