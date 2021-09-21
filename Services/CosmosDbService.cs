using LatencyAPI.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatencyAPI.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _pingContainer;
        private Container _collatorContainer;
        public void setCollatorContainer(Container container)
        {
            _collatorContainer = container;
        }
        public CosmosDbService(CosmosClient client, string db, string pingContainer, string collatorContainer)
        {
            this._pingContainer = client.GetContainer(db, pingContainer);
            this._collatorContainer = client.GetContainer(db, collatorContainer);

        }


        public async Task<CollatorLog> CreateCollatorLog(double latency, string region, DateTime time, string uuid)
        {
            CollatorLog log = new CollatorLog
            {
                id = System.Guid.NewGuid().ToString(),
                Latency = latency,
                Region = region,
                Timestamp = time,
                UUID = uuid
            };
            var result = await _collatorContainer.CreateItemAsync(log, null, null, new System.Threading.CancellationToken());

            if (!result.StatusCode.ToString().Equals("Created"))
            {
                return null;
            }
            return log;
        }

        public async Task<PingLog> CreatePingLog(string region, string originRegion, string uuid)
        {
            //var sql = "SELECT * FROM c";
            //var iterator = _pingContainer.GetItemQueryIterator<dynamic>(sql);
            //var page = await iterator.ReadNextAsync();
            //List<dynamic> logs = new List<dynamic>();
            PingLog log = new PingLog
            {
                id = System.Guid.NewGuid().ToString(),
                Region = region,
                OriginRegion = originRegion,
                UUID = uuid,
                Timestamp = DateTime.Now
            };
            var result = await _pingContainer.CreateItemAsync(log);
            if (!result.StatusCode.ToString().Equals("Created"))
            {
                return null;
            }
            return log;
        }
    }
}
