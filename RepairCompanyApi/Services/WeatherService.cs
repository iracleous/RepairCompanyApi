using RepairCompanyApi.Models;
using RepairCompanyApi.Repository;

namespace RepairCompanyApi.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _weatherRepository;

    public WeatherService(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public WeatherForecast CreateWeatherForecast(WeatherForecast forecast)
    {
        return _weatherRepository.Create(forecast);
    }

    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        return _weatherRepository.GetAll();
    }
}