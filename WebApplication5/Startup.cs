using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Database;
using WebApplication5.Database.Entities;

namespace WebApplication5
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication5", Version = "v1" });
            });

            services.AddDbContext<ApiDbContext>(
                options => options.UseInMemoryDatabase("PostalService")
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication5 v1"));
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApiDbContext>();

                dbContext.Parcels.Add(
                new Parcel
                {
                    Addressee = "Jan Kowalski",
                    Address = "Miodowa 13A/27",
                    City = "Kraków",
                    PostalCode = "30-690",
                    Country = "Poland",
                    Weight = 1.6,
                    ContactNumber = "1234567689",
                    RegisteredDate = DateTimeOffset.Parse("2021-12-16"),
                    ShippedDate = DateTimeOffset.Parse("2021-12-18"),
                    DeliveredDate = DateTimeOffset.Parse("2022-01-01"),
                }); ;

                dbContext.Parcels.Add(
                    new Parcel
                    {
                        Addressee = "Marcin Brzeg",
                        Address = "Topolowa 10",
                        City = "Warszawa",
                        PostalCode = "01-200",
                        Country = "Poland",
                        Weight = 20.5,
                        ContactNumber = "999888777",
                        RegisteredDate = DateTimeOffset.Parse("2021-12-18"),
                    });

                dbContext.Parcels.Add(
                    new Parcel
                    {
                        Addressee = "Andrzej Piana",
                        Address = "Krakowska 21/37",
                        City = "Tarnów",
                        PostalCode = "12-345",
                        Country = "Poland",
                        Weight = 0.2,
                        ContactNumber = "111222333",
                        RegisteredDate = DateTimeOffset.Parse("2021-12-31"),
                        ShippedDate = DateTimeOffset.Parse("2021-12-31"),
                    });

                dbContext.Parcels.Add(
                    new Parcel
                    {
                        Addressee = "Dominika Bies",
                        Address = "Bay 12",
                        City = "Montreal",
                        PostalCode = "220-20",
                        Country = "Canada",
                        Weight = 89.90,
                        ContactNumber = "12011101203",
                        RegisteredDate = DateTimeOffset.Parse("2022-01-01"),
                    });

                dbContext.SaveChanges();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
