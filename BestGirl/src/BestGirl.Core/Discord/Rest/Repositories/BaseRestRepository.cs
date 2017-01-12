using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace BestGirl.Core.Discord.Rest.Repositories
{
	public class BaseRestRepository
	{
		public HttpClient HttpClientClient { get; }
		public static readonly HttpMethod PatchMethod = new HttpMethod("PATCH");

		public BaseRestRepository(string baseUri, string authToken, string userAgent)
		{
			HttpClientClient = new HttpClient();
			HttpClientClient.BaseAddress = new Uri(baseUri.EndsWith("/") ? baseUri : baseUri + '/');
			HttpClientClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", authToken);
			HttpClientClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
		}

		protected async Task<HttpResponseMessage> GetAsync(string endpoint)
		{
			return await HttpClientClient.GetAsync(endpoint);
		}

		protected async Task<T> GetAsync<T>(string endpoint)
		{
			var response = await GetAsync(endpoint);
			var body = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(body);
		}

		protected async Task<HttpResponseMessage> PostJsonAsync(string endpoint, object body)
		{
			return await HttpClientClient.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
		}

		protected async Task<T> PostJsonAsync<T>(string endpoint, object body)
		{
			var response = await PostJsonAsync(endpoint, body);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(content);
		}

		protected async Task<HttpResponseMessage> PutJsonAsync(string endpoint, object body)
		{
			return await HttpClientClient.PutAsync(endpoint, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
		}

		protected async Task<T> PutJsonAsync<T>(string endpoint, object body)
		{
			var response = await PutJsonAsync(endpoint, body);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(content);
		}

		protected async Task<HttpResponseMessage> PatchJsonAsync(string endpoint, object body)
		{
			var request = new HttpRequestMessage(PatchMethod, endpoint)
			{
				Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
			};

			return await HttpClientClient.SendAsync(request);
		}

		protected async Task<T> PatchJsonAsync<T>(string endpoint, object body)
		{
			var response = await PatchJsonAsync(endpoint, body);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(content);
		}

		protected async Task<HttpResponseMessage> DeleteAsync(string endpoint)
		{
			return await HttpClientClient.DeleteAsync(endpoint);
		}

		protected async Task<T> DeleteAsync<T>(string endpoint)
		{
			var response = await DeleteAsync(endpoint);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(content);
		}
	}
}
