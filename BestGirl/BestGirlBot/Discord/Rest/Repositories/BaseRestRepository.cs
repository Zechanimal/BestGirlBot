﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace BestGirlBot.Discord.Rest.Repositories
{
	public class BaseRestRepository
	{
		protected HttpClient Client { get; set; }

		public BaseRestRepository(string baseUri, string authToken, string userAgent)
		{
			Client = new HttpClient();
			Client.BaseAddress = new Uri(baseUri.EndsWith("/") ? baseUri : baseUri + '/');
			Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", authToken);
			Client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
		}

		protected async Task<HttpResponseMessage> GetAsync(string endpoint)
		{
			return await Client.GetAsync(endpoint);
		}

		protected async Task<HttpResponseMessage> PostJsonAsync(string endpoint, object body)
		{
			return await Client.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
		}

		protected async Task<HttpResponseMessage> PutJsonAsync(string endpoint, object body)
		{
			return await Client.PutAsync(endpoint, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
		}

		protected async Task<HttpResponseMessage> PatchJsonAsync(string endpoint, object body)
		{
			var request = new HttpRequestMessage(new HttpMethod("PATCH"), endpoint)
			{
				Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
			};

			return await Client.SendAsync(request);
		}

		protected async Task<HttpResponseMessage> DeleteAsync(string endpoint)
		{
			return await Client.DeleteAsync(endpoint);
		}
	}
}
