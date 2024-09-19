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
        private readonly IServiceWeather _serviceWeather; 

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
           IServiceWeather serviceWeather)
        {
            _logger = logger;
            _serviceWeather = serviceWeather;
        }

        [HttpGet("Get")]
 
        public IEnumerable<WeatherForecast> Get()
        {
            return  _serviceWeather.GetWeatherForecast();
        }
    }
}
