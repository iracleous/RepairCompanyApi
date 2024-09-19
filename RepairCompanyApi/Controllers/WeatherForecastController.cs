using Microsoft.AspNetCore.Mvc;
using RepairCompanyApi.Models;
using RepairCompanyApi.Services;

namespace RepairCompanyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
         
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _serviceWeather; 

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
           IWeatherService serviceWeather)
        {
            _logger = logger;
            _serviceWeather = serviceWeather;
        }

        [HttpGet("Get")]
        public IEnumerable<WeatherForecast> Get()
        {
            return  _serviceWeather.GetWeatherForecast();
        }

        [HttpPost]
        public WeatherForecast Create(WeatherForecast forecast)
        {
            return _serviceWeather.CreateWeatherForecast(forecast);
        }


    }
}
