using Microsoft.AspNetCore.Mvc;

namespace HelloWorldApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

    [HttpGet]
        public string Get()
        {
            return "Hello from Day 1!";
        }
    }
}
