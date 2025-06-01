namespace MDDPlatform.RestClients;
public interface IRestClient
{
    Task<HttpResponseMessage> PostAsync(string uri,object data);
    Task<HttpResponseMessage> PostAsync(string uri);
    Task<T?> PostAsync<T>(string uri,object date);
    Task<T?> GetAsync<T>(string url);
    Task<HttpResponseMessage> DeleteAsync(string uri);
}