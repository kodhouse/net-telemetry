using System.Diagnostics.Metrics;



namespace MetricAggregator.Metrics;



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

    private List<Measurement<int>> hatalar;

    //private Measurement<int>[] hatalar;



    private ObservableGauge<int> TumSunucularHttpIstekGauge { get; }
    private int tumSunucularHttpIstek = 0;

    
    
    public AswMetrics()
    {
        var meter = new Meter("asw-metric");

        hatalar = new List<Measurement<int>>();
        
        ToplamSunucuGauge = meter.CreateObservableGauge<int>("toplam-sunucu-adedi", () => new Measurement<int>(toplamSunucu, new KeyValuePair<string, object?>("metric_type", "asw")));
        LogBulunanSunucuGauge = meter.CreateObservableGauge<int>("log-bulunan-sunucu-adedi", () => new Measurement<int>(logBulunanSunucu, new KeyValuePair<string, object?>("metric_type", "asw")));
        MaxBekleyenIstekGauge = meter.CreateObservableGauge<int>("max-bekleyen-istek", () => new Measurement<int>(maxBekleyenIstek, new KeyValuePair<string, object?>("metric_type", "asw")));
        
        TumSunucularHttpIstekGauge = meter.CreateObservableGauge<int>("tum-sunucular-http-istek", () => new Measurement<int>(tumSunucularHttpIstek, new KeyValuePair<string, object?>("metric_type", "asw")));

        HatalarGauge = meter.CreateObservableGauge<int>("hatalar", HatalariBul);

    }
    


    public void setHttpIstekAdedi(int val) => tumSunucularHttpIstek = val;
    public void setToplamSunucu(int val) => toplamSunucu = val;
    public void setLogBulunanSunucu(int val) => logBulunanSunucu = val;
    public void setMaxBekleyenIstek(int val) => maxBekleyenIstek = val;
    public void setMaxCpu(double val) => maxCpu = val;
    public void setMaxRam(double val) => maxRam= val;

    public void setHatalar(List<Measurement<int>> hataArr) => hatalar = hataArr;

    
     private List<Measurement<int>> HatalariBul() 
    {
        return hatalar;
    }

}





