using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BestGirlBot.Client.Services.Interfaces;
using BestGirlBot.Discord.Gateway;
using BestGirlBot.Discord.Rest;

namespace BestGirlBot.Client
{
	public partial class BestGirlClient
	{
		public BestGirlConfig Config { get; }
		public GatewaySocketClient GatewaySocketClient { get; }
		public RestClient RestClient { get; }
		private CancellationToken GatewayCancelToken { get; } = CancellationToken.None;

		private static readonly string DiscordHttpApiBaseUri = "https://discordapp.com/api/";

		private int _shardCount { get; set; } = 0;
		private int? _previousSequence { get; set; } = null;
		private bool _handshakeCompleted = false;
		private int? _heartbeatInterval = null;

		private ConcurrentDictionary<ulong, Models.Guild> _guilds;
		private ConcurrentDictionary<ulong, Models.Channel> _guildChannels;
		private ConcurrentDictionary<ulong, Models.Channel> _dmChannels;
		private ConcurrentDictionary<ulong, Models.User> _users;
		private ConcurrentDictionary<ulong, Models.Role> _roles;

		public IEnumerable<Models.Guild> Guilds => _guilds.Select(g => g.Value);
		public IEnumerable<Models.Channel> GuildChannels => _guildChannels.Select(c => c.Value);
		public IEnumerable<Models.Channel> DmChannels => _dmChannels.Select(c => c.Value);
		public IEnumerable<Models.User> Users => _users.Select(c => c.Value);
		public IEnumerable<Models.Role> Roles => _roles.Select(r => r.Value);
		public Models.User BotUser { get; private set; }

		public IEnumerable<IService> Services => _services.Select(s => s.Value);
		private Dictionary<string, IService> _services { get; set; }

		public BestGirlClient(BestGirlConfig config)
		{
			Config = config;
			RestClient = new RestClient(DiscordHttpApiBaseUri, Config.AuthToken, Config.UserAgent);

			GatewaySocketClient = new GatewaySocketClient();
			GatewaySocketClient.GatewayMessageReceived += OnGatewayMessageReceived;

			_guilds = new ConcurrentDictionary<ulong, Models.Guild>();
			_guildChannels = new ConcurrentDictionary<ulong, Models.Channel>();
			_dmChannels = new ConcurrentDictionary<ulong, Models.Channel>();
			_users = new ConcurrentDictionary<ulong, Models.User>();
			_roles = new ConcurrentDictionary<ulong, Models.Role>();

			_services = new Dictionary<string, IService>();
		}

		public async Task Connect()
		{
			var gatewayObject = RestClient.GetGatewayAsync().Result;
			var gatewayUrl = gatewayObject.Url;
			_shardCount = gatewayObject.Shards;

			foreach (var service in Services)
				service.Start(this);

			await GatewaySocketClient.Connect(gatewayUrl, GatewayCancelToken);
		}

		public void UseService(IService service)
		{
			if (_services.ContainsKey(service.Name))
			{
				throw new InvalidOperationException($"There is already a service named {service.Name} added");
			}

			_services[service.Name] = service;
		}

		private Models.User AddOrGetUser(Discord.Models.User user)
		{
			if (_users.ContainsKey(user.Id))
			{
				return _users[user.Id];
			}

			Models.User clientUser = new Models.User(this, user.Id, user.Username);
			_users[clientUser.Id] = clientUser;

			return clientUser;
		}
	}
}
