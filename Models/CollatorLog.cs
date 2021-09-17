using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatencyAPI.Models
{
    public class CollatorLog
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("Region")]
        public string Region { get; set; }
        [JsonProperty("Timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("Latency")]
        public double Latency { get; set; }
        [JsonProperty("UUID")]
        public string UUID { get; set; }
    }
}
