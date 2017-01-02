using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

		private ulong? _botUserId = null;

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

		public BestGirlClient(BestGirlConfig config)
		{
			Config = config;
			RestClient = new RestClient(DiscordHttpApiBaseUri, Config.AuthToken, Config.UserAgent);
			_botUserId = RestClient.GetCurrentUserAsync().Result.Id;

			GatewaySocketClient = new GatewaySocketClient();
			GatewaySocketClient.GatewayMessageReceived += OnGatewayMessageReceived;

			_guilds = new ConcurrentDictionary<ulong, Models.Guild>();
			_guildChannels = new ConcurrentDictionary<ulong, Models.Channel>();
			_dmChannels = new ConcurrentDictionary<ulong, Models.Channel>();
			_users = new ConcurrentDictionary<ulong, Models.User>();
			_roles = new ConcurrentDictionary<ulong, Models.Role>();
		}

		public async Task Connect()
		{
			var gatewayObject = RestClient.GetGatewayAsync().Result;
			var gatewayUrl = gatewayObject.Url;
			_shardCount = gatewayObject.Shards;

			await GatewaySocketClient.Connect(gatewayUrl, GatewayCancelToken);
		}
	}
}
