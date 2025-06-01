using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MDDPlatform.RestClients;
public class RestClient : IRestClient
{
    private readonly HttpClient _httpClient;

    public RestClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string uri)
    {
        return await _httpClient.GetFromJsonAsync<T?>(uri);
    }

    public async Task<HttpResponseMessage> PostAsync(string uri, object data)
    {
        var jsonData = JsonSerializer.Serialize(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync(uri, content);
    }
    public async Task<HttpResponseMessage> PostAsync(string uri)
    {
        return await _httpClient.PostAsync(uri, null);
    }
    public async Task<HttpResponseMessage> DeleteAsync(string uri)
    {
        return await _httpClient.DeleteAsync(uri);
    }

    public async Task<T?> PostAsync<T>(string uri, object data)
    {
        var jsonData = JsonSerializer.Serialize(data);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, content);
        if (!response.IsSuccessStatusCode)
            return default!;


        var stream = await response.Content.ReadAsStreamAsync();
        if (stream is null || stream.CanRead is false)
            return default!;

        JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
        serializerOptions.PropertyNameCaseInsensitive = true;
        return  await JsonSerializer.DeserializeAsync<T>(stream, serializerOptions);
    }
}