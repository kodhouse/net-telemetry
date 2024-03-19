using System.Diagnostics.Metrics;



namespace MetricAggregator.Metrics;



public class DbMetrics
{
    
    private ObservableGauge<int> AnlikSorguAdetGauge { get; }
    private int anlikSorguAdet = 0;
    private ObservableGauge<double> SqlServerCPUOrtalamaGauge { get; }
    private double sqlServerCPUOrtalama = 0;
    private ObservableGauge<double> OtherProcessCPUOrtalamaGauge { get; }
    private double otherProcessCPUOrtalama = 0;
    private ObservableGauge<double> ToplamSunucuCPUOrtalamaGauge { get; }
    private double toplamSunucuCPUOrtalama = 0.0;
    private ObservableGauge<double> BatchRequestPerSecondOrtalamaGauge { get; }
    private double batchRequestPerSecondOrtalama = 0.0;

    

    
    public DbMetrics()
    {
        var meter = new Meter("db-metric");
        
        AnlikSorguAdetGauge = meter.CreateObservableGauge<int>("anlik-sorgu-adet", () => new Measurement<int>(anlikSorguAdet, new KeyValuePair<string, object?>("metric_type", "db")));
        SqlServerCPUOrtalamaGauge = meter.CreateObservableGauge<double>("sql-server-cpu-ortalama", () => new Measurement<double>(sqlServerCPUOrtalama, new KeyValuePair<string, object?>("metric_type", "db")));
        OtherProcessCPUOrtalamaGauge = meter.CreateObservableGauge<double>("other-process-cpu-ortalama", () => new Measurement<double>(otherProcessCPUOrtalama, new KeyValuePair<string, object?>("metric_type", "db")));
        ToplamSunucuCPUOrtalamaGauge = meter.CreateObservableGauge<double>("toplam-sunucu-cpu-ortalama", () => new Measurement<double>(toplamSunucuCPUOrtalama, new KeyValuePair<string, object?>("metric_type", "db")));
        BatchRequestPerSecondOrtalamaGauge = meter.CreateObservableGauge<double>("batch-request-per-second-ortalama", () => new Measurement<double>(batchRequestPerSecondOrtalama, new KeyValuePair<string, object?>("metric_type", "db")));

    }


    public void setAnlikSorguAdet(int val) => anlikSorguAdet = val;
    public void setSqlServerCPUOrtalama(double val) => sqlServerCPUOrtalama = val;
    public void setOtherProcessCPUOrtalama(double val) => otherProcessCPUOrtalama = val;
    public void setToplamSunucuCPUOrtalama(double val) => toplamSunucuCPUOrtalama = val;
    public void setBatchRequestPerSecondOrtalama(double val) => batchRequestPerSecondOrtalama = val;
    
}





