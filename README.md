# Correlation ID Middleware

This repo contains middleware for syncing a TraceIdentity (correlation ID) across ASP.NET Core APIs.

This is intended for cases where you have multiple API services that may pass a single user request (transaction) through a chain of APIs in order to satisfy the final result. For example, a front end API may be called from a browser, which then in turn calls a backend API to gather some required data.

The TraceIdentifier on the HttpContext will be used for new requests and additionally set a header on the response. In cases where the incoming request includes an existing correlation ID in the header, the TraceIdentifier will be updated to that ID. This allows logging and diagnostics to be correlated for a single user transaction and to track the path of a user request through multiple API services.

This repository forked version of stevejgordon CorrelationID repository. 

I added CorrelationId headers to outgoing http calls to keep simple tracking through api to api calls. It uses correlation context for adding correlation id to outgoing http headers.  

HttpClient registiration is wrapped not to add outgoing delegating handlers every HttpClient object.

# Usage

```  
public void ConfigureServices(IServiceCollection services)
{
    services.AddCorrelationId();
    
    services.AddCustomHttpClient<ServiceAProxy>(cfg =>
                                                    {
                                                       cfg.BaseAddress = new Uri("http://localhost:51827");
                                                    });
    ......
}
```

```  
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
   app.UseCorrelationId(new CorrelationIdOptions
                                 {
                                     UseGuidForCorrelationId = true  //Default False
                                 });
    ......
}
```

## Known Issue with ASP.NET Core 2.2.0

It appears that a [regression in the code for ASP.NET Core 2.2.0](https://github.com/aspnet/AspNetCore/issues/5144) means that setting the TraceIdentifier on the context via middleware results in the context becoming null when accessed further down in the pipeline. A fix is ready for 3.0.0 and te team plan to back-port this for the 2.2.2 release timeframe

A workaround at this time is to disable the behaviour of updating the TraceIdentifier using the options when adding the middleware...

```
app.UseCorrelationId(new CorrelationIdOptions
{
	Header = "X-Correlation-ID",
	UseGuidForCorrelationId = true,
	UpdateTraceIdentifier = false
});
```

## Installation


