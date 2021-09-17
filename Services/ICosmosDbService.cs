using LatencyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatencyAPI.Services
{
    public interface ICosmosDbService
    {
        Task<PingLog> CreatePingLog(string region, string originRegion, string uuid);
        Task<CollatorLog>  CreateCollatorLog(double latency, string region, DateTime time, string uuid);
    }
}
