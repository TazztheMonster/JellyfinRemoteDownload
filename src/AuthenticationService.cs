using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JellyfinRemoteDownload
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> GetApiKey(string apiUrl, string username, string password)
        {
            var request = new
            {
                Username = username,
                Pw = password
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{apiUrl}/Users/AuthenticateByName", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            dynamic jsonResult = JsonConvert.DeserializeObject(result);
            return jsonResult.AccessToken;
        }
    }
}
