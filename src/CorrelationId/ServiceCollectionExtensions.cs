using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace CorrelationId
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddCustomHttpClient<T>(this IServiceCollection services, Action<HttpClient> action) where T : class
        {
            return services.AddHttpClient<T>()
           .AddHttpMessageHandler<CorrelationIdMessageHandler>()
           .ConfigureHttpClient(action);
        }

        public static IHttpClientBuilder AddCustomHttpClient<T, U>(this IServiceCollection services, Action<HttpClient> action) where U : class, T where T : class
        {
            return services.AddHttpClient<T, U>()
           .AddHttpMessageHandler<CorrelationIdMessageHandler>()
           .ConfigureHttpClient(action);
        }
    }
}
