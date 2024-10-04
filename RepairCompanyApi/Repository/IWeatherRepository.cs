using RepairCompanyApi.Models;

namespace RepairCompanyApi.Repository;

public interface IWeatherRepository
{
    WeatherForecast Create(WeatherForecast weatherForecast);
    WeatherForecast Update(WeatherForecast weatherForecast);
    bool Delete(int id);  
    WeatherForecast Get(int id);
    List<WeatherForecast> GetAll();
}
