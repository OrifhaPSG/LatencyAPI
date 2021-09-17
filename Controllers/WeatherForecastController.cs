using LatencyAPI.Models;
using LatencyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LatencyAPI.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICosmosDbService _cosmos;
        private readonly IConfiguration _config;
        private HttpClient http; 
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICosmosDbService cosmos, IConfiguration config)
        {
            _logger = logger;
            _cosmos = cosmos;
            _config = config;
            http = new HttpClient();
        }

        [HttpGet]
        [Route("ping")]
        public async Task<ActionResult<PingLog>> Ping()
        {
            string uuid = HttpContext.Request.Query["uuid"];
            string originRegion = HttpContext.Request.Query["region"];
            string region = _config["Region"];
            var status = await  _cosmos.CreatePingLog(region, originRegion, uuid);
            
            return  status;
            
        }

        [HttpGet]
        [Route("collator")]
        public async Task<CollatorLog> Collate()
        {
            string uuid = System.Guid.NewGuid().ToString();
            string pingServiceUrl = _config["PingServiceUrl"];
            string region = _config["Region"];
            string urlParams = "?uuid=" + uuid + "&region=" + region;
    
            if (http.BaseAddress == null)
            {
                http.BaseAddress = new Uri(pingServiceUrl);
                http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            }
            DateTime dateTime = DateTime.Now.AddMilliseconds(0);

            HttpResponseMessage response = http.GetAsync(urlParams).Result;
            if (response.IsSuccessStatusCode)
            {
                PingLog data = JsonConvert.DeserializeObject<PingLog>(response.Content.ReadAsStringAsync().Result);
                var latency = (data.Timestamp - dateTime).TotalMilliseconds;
                dateTime = DateTime.Now;
                var result = await _cosmos.CreateCollatorLog(latency, region, dateTime, uuid);
                return result;

            }
            return null;

        }
    }
}
