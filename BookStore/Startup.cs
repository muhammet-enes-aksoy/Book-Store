using System.Reflection;
using BookStore.DbOperations;
using BookStore.Middlewares;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;

namespace BookStore;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(); //Adds classes in the Controllers folder
        services.AddEndpointsApiExplorer(); // Discovers endpoints
        services.AddSwaggerGen(); //Prepares documentation for Swagger
        services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
        services.AddAutoMapper(Assembly.GetExecutingAssembly()); // AutoMapper Configurations
        services.AddSingleton<ILoggerService, ConsoleLogger>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment()) //If we are working in a development environment, UI is enabled.
        {
            app.UseDeveloperExceptionPage(); //If there is any mistake, let me know
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCustomExceptionMiddle(); //Custom Middle Ware
        app.UseEndpoints(x => { x.MapControllers(); }); 
    }
}