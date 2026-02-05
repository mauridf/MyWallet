using MyWallet.API.HealthChecks;
using MyWallet.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database");


// Controllers
builder.Services.AddControllers();

// Swagger completo
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HealthChecks
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Healthcheck endpoint
app.MapHealthChecks("/health");

app.Run();