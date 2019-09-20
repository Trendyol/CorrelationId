using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CorrelationId
{
    public class CorrelationIdMessageHandler : DelegatingHandler
    {
        private readonly ICorrelationContextAccessor _correlationContext;

        private readonly string _headerName = "X-Correlation-ID";

        public CorrelationIdMessageHandler(ICorrelationContextAccessor correlationContext)
        {
            _correlationContext = correlationContext;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains(_headerName))
            {
                request.Headers.Add(_headerName, _correlationContext.CorrelationContext.CorrelationId);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
