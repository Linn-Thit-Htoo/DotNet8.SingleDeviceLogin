using Shard.Infrastructure;
using Shared.DTOs.Features;

namespace DotNet8.SingleDeviceLogin.App.Services
{
	public class HttpClientService
	{
		private readonly HttpClient _httpClient;

		public HttpClientService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<T> ExecuteAsync<T>(string endpoint, EnumHttpMethod httpMethod, object? requestModel = null)
		{
			HttpResponseMessage? response = null;
			HttpContent? content = null;

			if (requestModel is not null)
			{
				string jsonStr = requestModel.SerializeObject();
				content = new StringContent(jsonStr);
			}

			switch (httpMethod)
			{
				case EnumHttpMethod.GET:
					response = await _httpClient.GetAsync(endpoint);
					break;
				case EnumHttpMethod.POST:
					response = await _httpClient.PostAsync(endpoint, content);
					break;
				case EnumHttpMethod.PUT:
					response = await _httpClient.PutAsync(endpoint, content);
					break;
				case EnumHttpMethod.PATCH:
					response = await _httpClient.PatchAsync(endpoint, content);
					break;
				case EnumHttpMethod.DELETE:
					response = await _httpClient.DeleteAsync(endpoint);
					break;
				case EnumHttpMethod.None:
				default:
					break;
			}

			string jsonResponse = await response!.Content.ReadAsStringAsync();
			return jsonResponse.DeserializeObject<T>();
		}
	}

	public enum EnumHttpMethod
	{
		None,
		GET,
		POST,
		PUT,
		PATCH,
		DELETE,
	}
}
