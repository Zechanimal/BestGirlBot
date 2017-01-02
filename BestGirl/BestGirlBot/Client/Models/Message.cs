using System.Collections.Generic;

namespace BestGirlBot.Client.Models
{
	public class Message
	{
		public ulong? Id { get; private set; }
		public Channel Channel { get; private set; }
		public Guild Guild { get; private set; }
		public User Author { get; private set; }
		public string Content { get; private set; }

		public Message(User author, Channel channel, string content, ulong? id = null)
		{
			Author = author;
			Channel = channel;
			Guild = channel.Guild;

			Content = content;
			Id = id;
		}
	}
}
