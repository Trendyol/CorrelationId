using System.Collections.Generic;
using System.Threading.Tasks;
using CorrelationId;
using Microsoft.AspNetCore.Mvc;

namespace MvcCorrelationIdSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ScopedClass _scoped;
        private readonly TransientClass _transient;
        private readonly SingletonClass _singleton;
        private readonly ICorrelationContextAccessor _correlationContext;
        private readonly IServiceAProxy _serviceAProxy;

        public ValuesController(ScopedClass scoped,
            TransientClass transient,
            SingletonClass singleton,
            ServiceAProxy serviceAProxy,
            ICorrelationContextAccessor correlationContext)
        {
            _scoped = scoped;
            _transient = transient;
            _singleton = singleton;
            _correlationContext = correlationContext;
            _serviceAProxy = serviceAProxy;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var correlation = _correlationContext.CorrelationContext.CorrelationId;

            await _serviceAProxy.Get();

            return new[]
            {
                $"DirectAccessor={correlation}",
                $"Transient={_transient.GetCorrelationFromScoped}",
                $"Scoped={_scoped.GetCorrelationFromScoped}",
                $"Singleton={_singleton.GetCorrelationFromScoped}",
                $"TraceIdentifier={HttpContext.TraceIdentifier}"
            };
        }
    }
}
