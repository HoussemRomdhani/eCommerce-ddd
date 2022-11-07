using eCommerce.Application;
using eCommerce.Domain;
using eCommerce.Domain.Countries;
using eCommerce.Domain.Customers.Repositories;
using eCommerce.Domain.Services;
using eCommerce.Domain.SharedKernel.Repositories;
using eCommerce.Infrastructure;
using Microsoft.OpenApi.Models;

namespace ecommerce.Apis.Customers;

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
        services.AddApplication();
        services.AddScoped(typeof(IReadRepositoryBase<>), typeof(InMemoryReadRepository<>));
        services.AddScoped(typeof(IRepositoryBase<>), typeof(InMemoryRepository<>));
        services.AddScoped<CheckoutService>();
        services.AddScoped<TaxService>();
        services.AddScoped(x => new Settings(Country.Create("FRA")));
        services.AddScoped<ICustomerRepository, InMemoryCustomerRepository>();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cart.API", Version = "v1" });
        });
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart.API v1"));

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

