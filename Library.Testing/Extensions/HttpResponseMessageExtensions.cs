using System.Net.Http;
using Newtonsoft.Json;

namespace Library.Testing.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static T GetResponseContent<T>(this HttpResponseMessage response)
        {
            var streamContent = response.Content.ReadAsStringAsync();
            streamContent.Wait();
            var stringContent = streamContent.Result;
            var deserializedResponseContent = JsonConvert.DeserializeObject<T>(stringContent);
            return deserializedResponseContent;
        }
    }
}