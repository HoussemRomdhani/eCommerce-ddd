using ecommerce.Apis.Customers.Extensions;
using eCommerce.Application;
using eCommerce.Domain;
using eCommerce.Domain.Countries;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Services;
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
        services.AddSingleton<CheckoutService>();
        services.AddSingleton<TaxService>();
        services.AddSingleton(x => new Settings(Country.Create("FRA", "France")));
        services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
        services.AddSingleton<ICountryRepository, InMemoryCountryRepository>();
        services.AddSingleton<IProductRepository, InMemoryProductRepository>();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customers.API", Version = "v1" });
        });
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseException();

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

