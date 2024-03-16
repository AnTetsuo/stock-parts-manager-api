
using stock_manager_api.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<StockManagerContext>();
builder.Services.AddScoped<IStockManagerContext, StockManagerContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
