using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using MetricAggregator.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var meterProvider1 = Sdk.CreateMeterProviderBuilder()
    .AddMeter("asw-metric")
    .AddPrometheusExporter()
    .Build();



var meterProvider2 = Sdk.CreateMeterProviderBuilder()
    .AddMeter("db-metric")
    .AddPrometheusExporter()
    .Build();    


builder.Services.AddSingleton(meterProvider1);
builder.Services.AddSingleton(meterProvider2);


var app = builder.Build();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/metrics/asw"), appBuilder => 
    {
        appBuilder.UseMiddleware<AswMiddleware>();
    });

app.UseWhen(context => context.Request.Path.StartsWithSegments("/metrics/db"), appBuilder => 
    {
        appBuilder.UseMiddleware<DbMiddleware>();
    });



app.UseOpenTelemetryPrometheusScrapingEndpoint(meterProvider1, context =>  context.Request.Path == "/metrics/asw",null, null, null);
app.UseOpenTelemetryPrometheusScrapingEndpoint(meterProvider2, context =>  context.Request.Path == "/metrics/db",null, null, null);




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
