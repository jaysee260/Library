using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library.Testing.Utilities
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _client;
        
        public HttpClientWrapper(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T payload)
        {
            var serializedPayload = JsonConvert.SerializeObject(payload);
            var response = await _client.PostAsync(url, new StringContent(serializedPayload, Encoding.UTF8, "application/json"));
            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await _client.DeleteAsync(url);
        }
    }
}