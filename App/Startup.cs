using System.Reflection;
using DataAccess;
using DataAccess.Services;
using Domain.Models;
using Domain.Models.Crm;
using Newtonsoft.Json;

namespace TryDiploma;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services.AddScoped<IService<ShoppingBag>, BagService>();
        services.AddScoped<IService<Beat>, BeatService>();
        services.AddScoped<IService<Client>, ClientService>();
        services.AddScoped<IService<Contract>, ContractService>();
        services.AddScoped<IService<Deal>, DealService>();
        services.AddScoped<IService<Funnel>, FunnelService>();
        services.AddScoped<IService<Section>, SectionService>();

        services.AddAutoMapper(typeof(AutoMapperProfile));
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        //var i = app.ApplicationServices.GetRequiredService<IService<Funnel>>();
        
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}