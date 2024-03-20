using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.OpenApi.Models;
using stock_manager_api;
using stock_manager_api.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<StockManagerContext>();
builder.Services.AddScoped<IStockManagerContext, StockManagerContext>();
builder.Services.AddScoped<CarRepository>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<AutoPartRepository>();
builder.Services.AddScoped<BudgetRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    swag => 
    {
        swag.SwaggerDoc("v1", new OpenApiInfo
        { 
            Title = "Stock Manager Api", 
            Version = "v1", 
            Description = "Manage stock and budgeted autoparts"
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        swag.IncludeXmlComments(xmlPath);
    });
builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x
                    .JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Load .env file
DotNetEnv.Env.TraversePath().Load();
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swag =>
        {
            swag.SwaggerEndpoint("/swagger/v1/swagger.json","Stock Manager Api");
            swag.RoutePrefix = string.Empty;
        }
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
