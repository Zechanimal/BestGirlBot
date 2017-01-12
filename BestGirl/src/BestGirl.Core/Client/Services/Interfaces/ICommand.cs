using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestGirl.Core.Client.Models;

namespace BestGirl.Core.Client.Services.Interfaces
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
		BeginningPhrase = 0x01,
		BeginningBotUserMention = 0x02,
		Regex = 0x04,
		CaseInvariant = 0x08
	}
}
