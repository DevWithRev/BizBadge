using BizBadgeApp.Models;
using System.Text.Json;

namespace BizBadgeApp.Services
{
    public class WeatherServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _apikey = "ae1ff15ab70b4b75887182916251307";
        public WeatherServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse> GetWeatherAsync( string city)
        {
            var url = $"http://api.weatherapi.com/v1/current.json?key={_apikey}&q={city}&aqi=yes";
            var response =await _httpClient.GetAsync(url);

            if(!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching weather data: {response.ReasonPhrase}");
            }
            var content = await response.Content.ReadAsStringAsync();

            
                var weather = JsonSerializer.Deserialize<WeatherResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return weather;
           


            

        }
    }
}
