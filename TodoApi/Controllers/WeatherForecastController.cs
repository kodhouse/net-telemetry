using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;

using System.Text;
using System.Text.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using TodoApi.QuickType;
using OpenTelemetry.Metrics;


namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private AswMetrics aswMetrics;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        aswMetrics = new AswMetrics();
    }

    //[HttpGet(Name = "GetWeatherForecast")]
    [HttpGet]
    [Route("GetAll")]
    public IEnumerable<WeatherForecast> Get()
    {   

        _logger.LogInformation("Getting weather forecast");
        var greeterActivitySource = new ActivitySource("greet-example-metric");
        // Create a new Activity scoped to the method
        using var activity = greeterActivitySource.StartActivity("GreeterActivity");

        //activity?.SetTag("operation.value", 1);
        
        //var greeterMeter = new Meter("greet-example-metric", "1.0.0");
        // Log a message
        _logger.LogInformation("Sending greeting");
        //var countGreetings = greeterMeter.CreateCounter<int>("greetings.count", description: "Counts the number of greetings");
        // Increment the custom counter
        //countGreetings.Add(1);

        //MeasurementConsumer.collect(reader)

        AswMetricsModel aswMetricsModel = GetAswMetricsModel();
        
        updateAswMetrics(aswMetricsModel);



        // Add a tag to the Activity
        activity?.SetTag("greeting", "Hello World!");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();


        /*
        _logger.LogInformation("Getting weather forecast");
        var greeterActivitySource = new ActivitySource("greet-example-metric");
        // Create a new Activity scoped to the method
        using var activity = greeterActivitySource.StartActivity("GreeterActivity");

        //activity?.SetTag("operation.value", 1);
        
        //var greeterMeter = new Meter("greet-example-metric", "1.0.0");
        // Log a message
        _logger.LogInformation("Sending greeting");
        //var countGreetings = greeterMeter.CreateCounter<int>("greetings.count", description: "Counts the number of greetings");
        // Increment the custom counter
        //countGreetings.Add(1);


        AswMetricsModel aswMetricsModel = GetAswMetricsModel();
        
        updateAswMetrics(aswMetricsModel);



        // Add a tag to the Activity
        activity?.SetTag("greeting", "Hello World!");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        */
    }


    //[HttpGet]
    //[Route("Update")]
    public AswMetricsModel GetAswMetricsModel() {
                
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var client = new HttpClient();

        var authenticationString = $"{"monitor.ada"}:{"Kb.!rew68XwzK"}";
        var base64String = Convert.ToBase64String(
                   System.Text.Encoding.ASCII.GetBytes(authenticationString));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

        client.BaseAddress = new Uri("https://monitor.ada:Kb.!rew68XwzK@portal.anasigorta.com.tr/");

        var content = new StringContent("[1]", Encoding.UTF8, "application/json");

        using HttpResponseMessage response = client.PostAsync("SistemMonitor.SonDakikalarSistemDurumAl.ada", content).Result;

        //var jsonResponse =  response.Content.ReadAsStringAsync();

        var responseContent = response.Content.ReadAsStringAsync().Result;

        var aswMetricsModel = AswMetricsModel.FromJson(responseContent);

        return aswMetricsModel;    
    }

    public void updateAswMetrics(AswMetricsModel aswMetricsModel) {
        aswMetrics.setHttpIstekAdedi(aswMetricsModel.AswMetrics.TumSunucularHttpIstekAdet);
        aswMetrics.setToplamSunucu(aswMetricsModel.AswMetrics.ToplamSunucuAdedi);
        aswMetrics.setLogBulunanSunucu(aswMetricsModel.AswMetrics.LogBulunanSunucuAdedi);
        aswMetrics.setMaxBekleyenIstek(aswMetricsModel.AswMetrics.MaxBekleyenIstek);
        aswMetrics.setMaxCpu(aswMetricsModel.AswMetrics.MaxCpu);
        aswMetrics.setMaxRam(aswMetricsModel.AswMetrics.MaxRam);

        //aswMetrics.HataEkle(aswMetricsModel.AswMetrics.Hatalar);


    }
}
