using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcCorrelationIdSample
{
    public interface IServiceAProxy
    {
        Task Get();
    }

    public class ServiceAProxy : IServiceAProxy
    {
        private readonly HttpClient _client;

        public ServiceAProxy(HttpClient client)
        {
            _client = client;
        }

        public async Task Get() => await _client.GetAsync("api/values");
    }
}
