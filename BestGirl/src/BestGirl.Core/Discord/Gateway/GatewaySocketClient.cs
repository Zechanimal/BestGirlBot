using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BestGirl.Core.Discord.Gateway
{
	public class GatewaySocketClient
	{
		private ClientWebSocket WebSocket { get; }
		private ConcurrentQueue<string> Messages { get; }
		private int MaxReceiveBytes = 4096;
		private int MaxSendBytes = 4096;

		public event EventHandler<GatewayMessageEventArgs> GatewayMessageReceived = delegate { };
		private void OnGatewayMessageReceived(GatewayMessageEventArgs e)
		{
			if (GatewayMessageReceived != null)
			{
				GatewayMessageReceived(this, e);
			}
		}

		public GatewaySocketClient()
		{
			Messages = new ConcurrentQueue<string>();
			WebSocket = new ClientWebSocket();
		}

		public async Task Connect(string host, CancellationToken cancellationToken)
		{
			await WebSocket.ConnectAsync(new Uri($"{host}/?encoding=json&v=5"), cancellationToken)
				.ContinueWith(t => Task.WaitAll(ReceiveAsync(cancellationToken), SendAsync(cancellationToken)));
		}

		public void SendMessage(string message)
		{
			Messages.Enqueue(message);
		}

		public void SendMessage(GatewayMessage message)
		{
			Messages.Enqueue(JsonConvert.SerializeObject(message));
		}

		private Task ReceiveAsync(CancellationToken cancellationToken)
		{
			return Task.Run(async () =>
			{
				var segment = new ArraySegment<byte>(new byte[MaxReceiveBytes]);
				var stream = new MemoryStream();

				while (!cancellationToken.IsCancellationRequested)
				{
					var result = await WebSocket.ReceiveAsync(segment, cancellationToken);
					if (result.MessageType == WebSocketMessageType.Close)
					{
						await WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close message receievd", cancellationToken);
						return;
					}

					stream.Write(segment.Array, 0, result.Count);
					while (!result.EndOfMessage)
					{
						result = await WebSocket.ReceiveAsync(segment, cancellationToken);
						stream.Write(segment.Array, 0, result.Count);
					}

					var messageBytes = stream.ToArray();
					string receivedMessage = null;
					receivedMessage = (result.MessageType == WebSocketMessageType.Binary)
						? Utility.Deflate(messageBytes)
						: Encoding.UTF8.GetString(messageBytes, 0, messageBytes.Length);

					var gatewayMessage = JsonConvert.DeserializeObject<GatewayMessage>(receivedMessage);
					OnGatewayMessageReceived(new GatewayMessageEventArgs(gatewayMessage));

					stream.Position = 0;
					stream.SetLength(0);
				}
			});
		}

		private Task SendAsync(CancellationToken cancellationToken)
		{
			return Task.Run(async () =>
			{
				while (!cancellationToken.IsCancellationRequested)
				{
					string message;
					while (Messages.TryDequeue(out message))
					{
						var bytes = Encoding.UTF8.GetBytes(message);
						int count = bytes.Length;
						int offset = 0;

						while (offset < count)
						{
							int chunkSize = count - offset >= MaxSendBytes ? MaxSendBytes : count - offset;
							bool lastChunk = offset + chunkSize >= count;

							var segment = new ArraySegment<byte>(bytes, offset, chunkSize);
							await WebSocket.SendAsync(segment, WebSocketMessageType.Text, lastChunk, cancellationToken);

							offset += chunkSize;
						}
					}

					await Task.Delay(100, cancellationToken).ConfigureAwait(false);
				}
			});
		}
	}
}
