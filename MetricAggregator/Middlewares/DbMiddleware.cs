using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;

using System.Text;
using System.Text.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using MetricAggregator.QuickType;
using MetricAggregator.Metrics;


namespace MetricAggregator.Middlewares;

public class DbMiddleware
{

    private readonly RequestDelegate _next;

    private Metrics.DbMetrics dbMetrics;

    public DbMiddleware(RequestDelegate next) {
        _next = next;
        dbMetrics = new Metrics.DbMetrics();
    }

    public async Task InvokeAsync(HttpContext context) {

        AswMetricsModel aswMetricsModel = GetAswMetricsModel();

        if (aswMetricsModel == null) {
            await _next(context);
            return;
        }
        
        updateAswMetrics(aswMetricsModel);


        await _next(context);
    }


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

        var responseContent = response.Content.ReadAsStringAsync().Result;

        var aswMetricsModel = AswMetricsModel.FromJson(responseContent);

         if (aswMetricsModel.ExceptionVar) {
            Console.WriteLine("DB metriclerini okurken hata olustu: " + aswMetricsModel.Mesaj + ", status code: " +aswMetricsModel.HttpResponseStatusCode);
            return null;
        }

        return aswMetricsModel;    
    }

    public void updateAswMetrics(AswMetricsModel aswMetricsModel) {
        dbMetrics.setAnlikSorguAdet(aswMetricsModel.DbMetrics.AnlikSorguAdet);
        dbMetrics.setSqlServerCPUOrtalama(aswMetricsModel.DbMetrics.SqlServerCpuOrtalama);
        dbMetrics.setOtherProcessCPUOrtalama(aswMetricsModel.DbMetrics.OtherProcessCpuOrtalama);
        dbMetrics.setToplamSunucuCPUOrtalama(aswMetricsModel.DbMetrics.ToplamSunucuCpuOrtalama);
        dbMetrics.setBatchRequestPerSecondOrtalama(aswMetricsModel.DbMetrics.BatchRequestPerSecondOrtalama);
    }

}