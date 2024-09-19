using RepairCompanyApi.Models;

namespace RepairCompanyApi.Services;

public interface IServiceWeather
{
    public IEnumerable<WeatherForecast> GetWeatherForecast();
}
