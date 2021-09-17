using LatencyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatencyAPI.Services
{
    public interface IPingService
    {
        PingLog getPing();
    }
}
