using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Microsoft.Extensions.Configuration;
using TodoApi.QuickType;


namespace TodoApi;



public class AswMetrics
{
    
    private ObservableGauge<int> ToplamSunucuGauge { get; }
    private int toplamSunucu = 0;
    private ObservableGauge<int> LogBulunanSunucuGauge { get; }
    private int logBulunanSunucu = 0;
    private ObservableGauge<int> MaxBekleyenIstekGauge { get; }
    private int maxBekleyenIstek = 0;
    private ObservableGauge<double> MaxCpuGauge { get; }
    private double maxCpu = 0.0;
    private ObservableGauge<double> MaxRamGauge { get; }
    private double maxRam = 0.0;

    
    private ObservableGauge<int> HatalarGauge { get;}




    private ObservableGauge<int> TumSunucularHttpIstekGauge { get; }
    private int tumSunucularHttpIstek = 0;

    

    public AswMetrics()
    {
        var meter = new Meter("greet-example-metric", "1.0.0");

        
        ToplamSunucuGauge = meter.CreateObservableGauge<int>("toplam-sunucu-adedi", () => toplamSunucu);
        LogBulunanSunucuGauge = meter.CreateObservableGauge<int>("log-bulunan-sunucu-adedi", () => logBulunanSunucu);
        //MaxBekleyenIstekGauge = meter.CreateObservableGauge<int>("max-bekleyen-istek", () => maxBekleyenIstek, new KeyValuePair<string, string>("label1", "lll"));
        MaxBekleyenIstekGauge = meter.CreateObservableGauge<int>("max-bekleyen-istek", () => maxBekleyenIstek, new KeyValuePair<string, string>("label1", "lll"));
        MaxCpuGauge = meter.CreateObservableGauge<double>("max-cpu", () => maxCpu);
        MaxRamGauge = meter.CreateObservableGauge<double>("max-ram", () => maxRam);
        
        TumSunucularHttpIstekGauge = meter.CreateObservableGauge<int>("tum-sunucular-http-istek", () => tumSunucularHttpIstek);



        HatalarGauge = meter.CreateObservableGauge<int>("hatalar", HatalariBul);
        
        //Measurement<double>
    }


    public void setHttpIstekAdedi(int val) => tumSunucularHttpIstek = val;
    public void setToplamSunucu(int val) => toplamSunucu = val;
    public void setLogBulunanSunucu(int val) => logBulunanSunucu = val;
    public void setMaxBekleyenIstek(int val) => maxBekleyenIstek = val;
    public void setMaxCpu(double val) => maxCpu = val;
    public void setMaxRam(double val) => maxRam= val;


    
    private IEnumerable<Measurement<int>> HatalariBul() 
    {
        return
        [
            new Measurement<int>(10, new KeyValuePair<string, object>("hata_baslik", "The file '/WebServisleri/hata.aspx' does not exist.")),
            new Measurement<int>(5, new KeyValuePair<string, object>("hata_baslik", "Request format is unrecognized."))
        ];
    }

    /*
    public String getHttpIstekAdediInPrometheusFormat() {

        return "# TYPE " + TumSunucularHttpIstekGauge.Name +" " + TumSunucularHttpIstekGauge. + "\n" + ;
    }
    */
    
    public String getHttpIstekAdediInPrometheusFormat() {

        return "# TYPE " + TumSunucularHttpIstekGauge.Name +" gauge"  + "\n"
                + TumSunucularHttpIstekGauge.Name + " " + tumSunucularHttpIstek ;
    }

    
    /*
    public void HataEkle(Hatalar[] hatalar) 
    {
        for (int i = 0; i < hatalar.Length; i++) {
            
            KeyValuePair<string, object> pair = new KeyValuePair<string, object>("hata_baslik", hatalar[i].Baslik);

            if (!HatalarGauge.Tags.Contains(pair)) {
                HatalarGauge.Tags.Append(pair);
            }

            //HatalarGauge.Observe();
        }
    }

    */
    






    


}





