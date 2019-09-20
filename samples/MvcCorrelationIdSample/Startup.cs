﻿using CorrelationId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Web.Http;

namespace MvcCorrelationIdSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCorrelationId();

            services.AddCustomHttpClient<ServiceAProxy>(cfg =>
           {
               cfg.BaseAddress = new Uri("http://xxxxxxx.com");
           });

            services.AddScoped<ScopedClass>();
            services.AddTransient<TransientClass>();
            services.AddSingleton<SingletonClass>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorrelationId();

            app.UseMvc();
        }
    }

    
}
