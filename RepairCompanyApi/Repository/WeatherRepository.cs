using RepairCompanyApi.Data;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Repository;

public class WeatherRepository : IWeatherRepository
{

    private readonly  RepairDbContext _repairDbContext;

    public WeatherRepository(RepairDbContext repairDbContext)
    {
        _repairDbContext = repairDbContext;
    }

    private static readonly string[] Summaries =
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };



    public WeatherForecast Create(WeatherForecast weatherForecast)
    {
        _repairDbContext.WeatherForecasts.Add(weatherForecast);
        _repairDbContext.SaveChanges();
        return weatherForecast;
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public WeatherForecast Get(int id)
    {
        throw new NotImplementedException();
    }

    public List<WeatherForecast> GetAll()
    {
        //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //{
        //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //    TemperatureC = Random.Shared.Next(-20, 55),
        //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //})
        //    .ToList();

        return _repairDbContext
            .WeatherForecasts
            .ToList();
    }

    public WeatherForecast Update(WeatherForecast weatherForecast)
    {
        throw new NotImplementedException();
    }
}
