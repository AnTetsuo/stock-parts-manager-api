using System.Text.Json.Serialization;
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
builder.Services.AddSwaggerGen();
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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
