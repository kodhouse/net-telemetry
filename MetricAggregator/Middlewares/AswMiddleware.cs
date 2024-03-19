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

public class AswMiddleware
{

    private readonly RequestDelegate _next;

    private Metrics.AswMetrics aswMetrics;

    public AswMiddleware(RequestDelegate next) {
        _next = next;
        aswMetrics = new Metrics.AswMetrics();
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
            Console.WriteLine("ASW metriclerini okurken hata olustu: " + aswMetricsModel.Mesaj + ", status code: " +aswMetricsModel.HttpResponseStatusCode);
            return null;
        }

        return aswMetricsModel;    
    }

    public void updateAswMetrics(AswMetricsModel aswMetricsModel) {
        aswMetrics.setHttpIstekAdedi(aswMetricsModel.AswMetrics.TumSunucularHttpIstekAdet);
        aswMetrics.setToplamSunucu(aswMetricsModel.AswMetrics.ToplamSunucuAdedi);
        aswMetrics.setLogBulunanSunucu(aswMetricsModel.AswMetrics.LogBulunanSunucuAdedi);
        aswMetrics.setMaxBekleyenIstek(aswMetricsModel.AswMetrics.MaxBekleyenIstek);
        aswMetrics.setMaxCpu(aswMetricsModel.AswMetrics.MaxCpu);
        aswMetrics.setMaxRam(aswMetricsModel.AswMetrics.MaxRam);

        aswMetrics.setHatalar(labelHatalar(aswMetricsModel.AswMetrics.Hatalar));
    }

    private List<Measurement<int>> labelHatalar (Hatalar[] hatalar) {
        
        List<Measurement<int>> hataList = new List<Measurement<int>>();

        /*
        if (hatalar.Length == 0) {
            hataList.Add(new Measurement<int>(0, new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("hata_baslik", "hata yok"),
                                    new KeyValuePair<string, object?>("metric_type", "asw")}));
            return hataList;
        }
        */

        for (int i = 0; i < hatalar.Length; i++) {
            hataList.Add(new Measurement<int>(hatalar[i].Adet, new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("hata_baslik", hatalar[i].Baslik),
                                    new KeyValuePair<string, object?>("metric_type", "asw")}));
        }
        
        return hataList;
    }

}