using System;
using System.Collections.Generic;

namespace RepairCompanyApi.Models2;

public partial class WeatherForecast
{
    public long Id { get; set; }

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public string? Summary { get; set; }
}
