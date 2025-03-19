using Newtonsoft.Json;
using Site.Models;

namespace Site
{
    public class WeatherService
    {
        HttpClient _client;
        public WeatherService()
        {
            _client = new HttpClient();

        }

        public async Task<WeatherData> GetWeatherData(string query)
        {
            WeatherData weatherData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return weatherData;
        }
    }
}
