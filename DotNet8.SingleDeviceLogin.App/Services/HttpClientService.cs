using Blazored.LocalStorage;
using Shard.Infrastructure;
using Shared.DTOs.Features;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DotNet8.SingleDeviceLogin.App.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public HttpClientService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<T> ExecuteAsync<T>(string endpoint, EnumHttpMethod httpMethod, object? requestModel = null)
        {
            HttpResponseMessage? response = null;
            HttpContent? content = null;
            string? token = await _localStorage.GetItemAsStringAsync("token");

            if (token is not null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            if (requestModel is not null)
            {
                string jsonStr = requestModel.SerializeObject();
                content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
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
