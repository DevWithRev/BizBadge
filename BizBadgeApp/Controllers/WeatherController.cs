using BizBadgeApp.Models;
using BizBadgeApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BizBadgeApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherServices _weatherResponse;

        public WeatherController(WeatherServices weatherResponse)
        {
            _weatherResponse = weatherResponse;
        }
        public async Task<IActionResult> Index( string city)
        {
            var weather = await _weatherResponse.GetWeatherAsync(city);
            return View(weather);
        }
    }
}
