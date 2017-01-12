using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BestGirl.Core.Client.Models;
using BestGirl.Core.Client.Services.Interfaces;
using System;
using Newtonsoft.Json;

namespace BestGirl.Core.Client.Services
{
	public abstract class Command : ICommand
	{
		public abstract string Name { get; }
		public abstract string Description { get; }

		public abstract CommandConvention CallingConvention { get; }

		public abstract IEnumerable<string> CommandTriggers { get; }

		public bool CanHandleMessage(BestGirlClient client, Message message)
		{
			if (message.Author == client.BotUser) return false;

			var args = GetMessageArgs(message);

			if ((CallingConvention & CommandConvention.BeginningPhrase) == CommandConvention.BeginningPhrase)
			{
				if (CallingConvention.HasFlag(CommandConvention.CaseInvariant))
				{
					foreach (var trigger in CommandTriggers)
						if (CultureInfo.InvariantCulture.CompareInfo.IndexOf(trigger, args.First(), CompareOptions.IgnoreCase) >= 0) return true;
				}
				else
				{
					if (CommandTriggers.Contains(args.First())) return true;
				}
			}
			else if ((CallingConvention & CommandConvention.BeginningBotUserMention) == CommandConvention.BeginningBotUserMention)
			{
				if (args.First() == client.BotUser.Mention)
				{
					if (args.Count() > 1 && CommandTriggers.Contains(args[1])) return true;
				}
			}
			else if (CallingConvention.HasFlag(CommandConvention.Regex))
			{
				foreach (var trigger in CommandTriggers)
				{
					var regex = new Regex(trigger, CallingConvention.HasFlag(CommandConvention.CaseInvariant) ? RegexOptions.IgnoreCase : RegexOptions.None);
					if (regex.IsMatch(message.Content)) return true;
				}
			}

			return false;
		}

		public abstract Task HandleMessage(BestGirlClient cilent, Message message);

		public List<string> GetMessageArgs(Message message)
		{
			return Utility.ReduceWhitespace(message.Content).Split(' ').ToList();
		}
	}
}
