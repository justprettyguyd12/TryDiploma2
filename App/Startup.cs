using System.Reflection;
using System.Security.Claims;
using DataAccess;
using DataAccess.Services;
using Domain.Models;
using Domain.Models.Crm;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TryDiploma.Data;
using TryDiploma.Data.Entities;

namespace TryDiploma;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //Basic settings
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
        
        //Users & authentification settings
        services.AddDbContext<UsersContext>(config =>
            {
                config.UseNpgsql("Host=localhost;Port=5432;Database=beatmaker-users;Username=postgres;Password=123")
                    .EnableSensitiveDataLogging();
            }).AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 3;
                config.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<UsersContext>();
        services.ConfigureApplicationCookie(config =>
        {
            config.LoginPath = "/Admin/Login";
            config.AccessDeniedPath = "/Home/AccessDenied";
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Developer", builder => 
                builder.RequireClaim(ClaimTypes.Role, "Developer"));
            options.AddPolicy("Administrator", builder =>
                builder.RequireAssertion(x => 
                    x.User.HasClaim(ClaimTypes.Role, "Administrator")
                    || x.User.HasClaim(ClaimTypes.Role, "Developer")));
            options.AddPolicy("Client", builder =>
                builder.RequireAssertion(x => 
                    x.User.HasClaim(ClaimTypes.Role, "Client")
                    || x.User.HasClaim(ClaimTypes.Role, "Developer")));
        });
        
        //Services settings for main DataBases
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

        app.UseStaticFiles();
        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}