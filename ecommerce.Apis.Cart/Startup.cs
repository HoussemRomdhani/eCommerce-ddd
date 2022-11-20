using eCommerce.Application;
using eCommerce.Domain;
using eCommerce.Domain.Carts;
using eCommerce.Domain.Countries;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Purchases;
using eCommerce.Domain.Services;
using eCommerce.Domain.Tax;
using eCommerce.Infrastructure;
using Microsoft.OpenApi.Models;

namespace ecommerce.Apis.Cart;

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
        services.AddSingleton<ICartRepository, InMemoryCartRepository>();
        services.AddSingleton<ICountryTaxRepository, InMemoryCountryTaxRepository>();
        services.AddSingleton<IPurchaseRepository, InMemoryPurchaseRepository>();

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

