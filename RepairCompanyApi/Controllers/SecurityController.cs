using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepairCompanyApi.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepairCompanyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SecurityController : ControllerBase
{
    private readonly ILogger<SecurityController> _logger;

    public SecurityController(ILogger<SecurityController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    [Route("login")]
    public string GetToken([FromQuery(Name = "username")] string username, [FromQuery(Name = "password")] string password)
    {
        _logger.Log(LogLevel.Information, "GetToken starts/ends");
        return GenerateJwtToken(username, password);
    }



    [HttpGet]
    [Route("work")]
    [Authorize]
    public string GetAuthorize()
    {
        _logger.Log(LogLevel.Information, "GetAuthorize starts/ends");

        //analysis of a claim

        var userIdClaim = User.Claims.FirstOrDefault(
            c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        var userClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

        string userId = "";

        if (userClaim != null)
        {
            // Extract the user ID from the claim
            userId = userClaim.Value;

        }

        return userId;
    }

    [HttpGet]
    [Route("amonymous")]
    public string GetAnonymous()
    {
        return "anonymous";
}

        private static string GenerateJwtToken(string username, string password)
    {

        // Replace "your-secret-key" with your actual secret key

        // Convert the secret key to a byte array
        byte[] keyBytes = Encoding.UTF8.GetBytes(SecurityInfo.SecretKey);

        // Create a SymmetricSecurityKey using the byte array
        var key = new SymmetricSecurityKey(keyBytes);

        //HS256
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //check in database if the user exists

        var token = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: new[] { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Country, "Greece") },
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
