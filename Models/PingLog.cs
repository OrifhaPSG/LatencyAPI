using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatencyAPI.Models
{
    public class PingLog
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("Region")]
        public string Region { get; set; }
        [JsonProperty("OriginRegion")]
        public string OriginRegion { get; set; }
        [JsonProperty("Timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("ClientIP")]
        public string ClientIP { get; set; }
        [JsonProperty("UUID")]
        public string UUID { get; set; }
    }
}
