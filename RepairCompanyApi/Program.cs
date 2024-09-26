using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RepairCompanyApi.Data;
using RepairCompanyApi.Repository;
using RepairCompanyApi.Security;
using RepairCompanyApi.Services;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions
     .ReferenceHandler = ReferenceHandler.Preserve;
});


builder.Services.AddScoped<IWeatherService,WeatherService>();
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();

builder.Services.AddScoped<IPropertyOwnerService, PropertyOwnerService>();
builder.Services.AddDbContext<RepairDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//security 1/2
// adding authentication
/**
 * ASP.NET Core middleware that enables an application to 
 * receive an OpenID Connect bearer token.
 * // reading the header from a request and parsing it
 * */


var securitySecret = builder.Configuration.GetValue<string>("Security:Secret") 
    ?? SecurityInfo.SecretKey;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "your-issuer",  // Replace with your issuer
        ValidAudience = "your-audience",  // Replace with your audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitySecret))  // Replace with your secret key
    };
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
