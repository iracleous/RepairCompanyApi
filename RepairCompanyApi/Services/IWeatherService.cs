using Microsoft.Identity.Client;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Services;

public interface IWeatherService
{
    public IEnumerable<WeatherForecast> GetWeatherForecast();
    public WeatherForecast CreateWeatherForecast(WeatherForecast forecast); 
}
