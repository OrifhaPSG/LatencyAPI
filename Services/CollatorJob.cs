using LatencyAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatencyAPI.Services
{
    public class CollatorJob : IJob
    {
        private ILogger<LatencyController> _logger;
        private ICosmosDbService _cosmos;
        private IConfiguration _config;
        private LatencyController _controller;
        public CollatorJob(ILogger<LatencyController> logger, ICosmosDbService cosmos, IConfiguration config)
        {
            _logger = logger;
            _cosmos = cosmos;
            _config = config;
            _controller = new LatencyController(_logger, _cosmos, _config);

        }



        public async Task Execute(IJobExecutionContext ctx)
        {
            await _controller.Collate();
        }

        
    }
}
