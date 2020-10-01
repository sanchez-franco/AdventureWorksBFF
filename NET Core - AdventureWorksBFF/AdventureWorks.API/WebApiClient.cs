using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.API
{
    public class WebApiClient
    {
        private readonly string _token;

        public WebApiClient()
        {
        }

        public WebApiClient(string token)
        {
            _token = token;
        }

        private HttpClient GetHttpClient(string host, string token)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(host)
            };
            client.DefaultRequestHeaders.Accept.Clear();

            if (!string.IsNullOrWhiteSpace(token))
            {
                //Add the authorization header
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
            }

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private HttpContent GetHttpContent<TPostType>(TPostType model)
        {
            var myContent = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            return httpContent;
        }

        public async Task<TReturnType> GetAsync<TReturnType>(string host, string route, params string[] paramStrings)
        {
            using (var client = GetHttpClient(host, _token))
            {
                string url = $"{route}/{string.Join("/", paramStrings)}";

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TReturnType>(json);
                }

                return default;
            }
        }

        public async Task<TReturnType> Post<TPostType, TReturnType>(string host, string route, TPostType model)
        {
            using (var client = GetHttpClient(host, _token))
            {
                var content = GetHttpContent(model);

                var response = await client.PostAsync(route, content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TReturnType>(json);
                }

                return default;
            }
        }
    }
}
