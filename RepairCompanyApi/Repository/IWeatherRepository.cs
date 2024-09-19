using RepairCompanyApi.Models;

namespace RepairCompanyApi.Repository;

public interface IWeatherRepository
{
    //CRUD


    public WeatherForecast Create(WeatherForecast weatherForecast);
    public WeatherForecast Update(WeatherForecast weatherForecast);
    public bool Delete(int id);  
    public WeatherForecast Get(int id);
    public List<WeatherForecast> GetAll();


}
