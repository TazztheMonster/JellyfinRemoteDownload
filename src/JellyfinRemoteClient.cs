using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace JellyfinRemoteDownloadPlugin
{
    public class JellyfinRemoteClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public JellyfinRemoteClient(string apiUrl, string apiKey)
        {
            _httpClient = new HttpClient();
            _apiUrl = apiUrl;
            _apiKey = apiKey;
        }

        // Alle Serien abrufen
        public async Task<string> GetSeries()
        {
            var requestUrl = $"{_apiUrl}/Items?api_key={_apiKey}&Recursive=true&IncludeItemTypes=Series";
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // Episoden für eine Serie abrufen
        public async Task<string> GetEpisodes(string seriesId)
        {
            var requestUrl = $"{_apiUrl}/Shows/{seriesId}/Episodes?api_key={_apiKey}";
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // Alle Episoden einer Serie herunterladen und Ordnerstruktur erstellen
        public async Task DownloadSeries(string seriesId, string seriesName, string savePath)
        {
            var episodes = await GetEpisodes(seriesId);

            // Erstelle das Hauptverzeichnis für die Serie
            var seriesDirectory = Path.Combine(savePath, seriesName);
            Directory.CreateDirectory(seriesDirectory);

            foreach (var episode in episodes)
            {
                var seasonDirectory = Path.Combine(seriesDirectory, $"Season {episode.SeasonNumber}");
                Directory.CreateDirectory(seasonDirectory);

                var videoId = episode.Id;
                var episodePath = Path.Combine(seasonDirectory, $"{episode.Name}.mp4");

                // Herunterladen der Episode
                var requestUrl = $"{_apiUrl}/Videos/{videoId}/Stream?api_key={_apiKey}";
                var response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                using (var fileStream = System.IO.File.Create(episodePath))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
            }
        }
    }
}
