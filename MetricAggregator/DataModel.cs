namespace MetricAggregator.QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class AswMetricsModel
    {
        [JsonProperty("TarihBaslangic")]
        public DateTimeOffset TarihBaslangic { get; set; }

        [JsonProperty("TarihBitis")]
        public DateTimeOffset TarihBitis { get; set; }

        [JsonProperty("AswMetrics")]
        public AswMetrics AswMetrics { get; set; }

        [JsonProperty("DBMetrics")]
        public DbMetrics DbMetrics { get; set; }

        [JsonProperty("SigortaMetrics")]
        public SigortaMetrics SigortaMetrics { get; set; }

        [JsonProperty("BilgilendirmeMetrics")]
        public BilgilendirmeMetrics BilgilendirmeMetrics { get; set; }

        [JsonProperty("GenelSaglikKontrolSonucu")]
        public GenelSaglikKontrolSonucu GenelSaglikKontrolSonucu { get; set; }

        [JsonProperty("Mesaj")]
        public String Mesaj { get; set; }

        [JsonProperty("ExceptionVar")]
        public bool ExceptionVar { get; set; }


        [JsonProperty("HttpResponseStatusCode")]
        public int HttpResponseStatusCode { get; set; }
        
    }


    public partial class AswMetrics
    {
        [JsonProperty("ToplamSunucuAdedi")]
        public int ToplamSunucuAdedi { get; set; }

        [JsonProperty("LogBulunanSunucuAdedi")]
        public int LogBulunanSunucuAdedi { get; set; }

        [JsonProperty("MaxBekleyenIstek")]
        public int MaxBekleyenIstek { get; set; }

        [JsonProperty("MaxCPU")]
        public double MaxCpu { get; set; }

        [JsonProperty("MaxRAM")]
        public double MaxRam { get; set; }

        [JsonProperty("MaxBekleyenIstekSunucuAdi")]
        public string MaxBekleyenIstekSunucuAdi { get; set; }

        [JsonProperty("MaxCPUSunucuAdi")]
        public string MaxCpuSunucuAdi { get; set; }

        [JsonProperty("MaxRAMSunucuAdi")]
        public string MaxRamSunucuAdi { get; set; }

        [JsonProperty("TumSunucularHttpIstekAdet")]
        public int TumSunucularHttpIstekAdet { get; set; }

        [JsonProperty("Hatalar")]
        public Hatalar[] Hatalar { get; set; }

        [JsonProperty("SunucuBilgi")]
        public SunucuBilgi[] SunucuBilgi { get; set; }

        [JsonProperty("RaporUygulamasiYanitVeriyor")]
        public bool RaporUygulamasiYanitVeriyor { get; set; }
    }

    public partial class Hatalar
    {
        [JsonProperty("Baslik")]
        public string Baslik { get; set; }

        [JsonProperty("Adet")]
        public int Adet { get; set; }
    }

    public partial class SunucuBilgi
    {
        [JsonProperty("SunucuAdi")]
        public string SunucuAdi { get; set; }

        [JsonProperty("HttpIstekAdet")]
        public int HttpIstekAdet { get; set; }

        [JsonProperty("LogAdetCpuRamIis")]
        public int LogAdetCpuRamIis { get; set; }

        [JsonProperty("LogAdetGuncelleme")]
        public int LogAdetGuncelleme { get; set; }

        [JsonProperty("MaxBekleyenIstek")]
        public int MaxBekleyenIstek { get; set; }

        [JsonProperty("MaxCPU")]
        public double MaxCpu { get; set; }

        [JsonProperty("MaxRAM")]
        public double MaxRam { get; set; }

        [JsonProperty("SunucularListesindeVar")]
        public bool SunucularListesindeVar { get; set; }
    }

    public partial class BilgilendirmeMetrics
    {
        [JsonProperty("OlusturulanEPostaAdedi")]
        public int OlusturulanEPostaAdedi { get; set; }

        [JsonProperty("OlusturulanSMSAdedi")]
        public int OlusturulanSmsAdedi { get; set; }

        [JsonProperty("GonderilenEPostaAdedi")]
        public int GonderilenEPostaAdedi { get; set; }

        [JsonProperty("GonderilenSMSAdedi")]
        public int GonderilenSmsAdedi { get; set; }

        [JsonProperty("EsikDakikaOncesiYaratilmisAmaGonderimDenenmemisEPostaAdedi")]
        public int EsikDakikaOncesiYaratilmisAmaGonderimDenenmemisEPostaAdedi { get; set; }

        [JsonProperty("EsikDadikaOncesiYaratilmisAmaGonderimDenenmemisSMSAdedi")]
        public int EsikDadikaOncesiYaratilmisAmaGonderimDenenmemisSmsAdedi { get; set; }

        [JsonProperty("EsikDakika")]
        public int EsikDakika { get; set; }
    }

    public partial class DbMetrics
    {
        [JsonProperty("AnlikSorguAdet")]
        public int AnlikSorguAdet { get; set; }

        [JsonProperty("SqlServerCPUOrtalama")]
        public double SqlServerCpuOrtalama { get; set; }

        [JsonProperty("OtherProcessCPUOrtalama")]
        public double OtherProcessCpuOrtalama { get; set; }

        [JsonProperty("ToplamSunucuCPUOrtalama")]
        public double ToplamSunucuCpuOrtalama { get; set; }

        [JsonProperty("BatchRequestPerSecondOrtalama")]
        public double BatchRequestPerSecondOrtalama { get; set; }

        [JsonProperty("DbaVeritabaniVar")]
        public bool DbaVeritabaniVar { get; set; }

        [JsonProperty("Notlar")]
        public object[] Notlar { get; set; }

        [JsonProperty("Replicalar")]
        public Replicalar[] Replicalar { get; set; }
    }

    public partial class Replicalar
    {
        [JsonProperty("SubscriberDb")]
        public string SubscriberDb { get; set; }

        [JsonProperty("Latency")]
        public int Latency { get; set; }
    }

    public partial class GenelSaglikKontrolSonucu
    {
        [JsonProperty("HerseyYolunda")]
        public bool HerseyYolunda { get; set; }

        [JsonProperty("Sorunlar")]
        public object[] Sorunlar { get; set; }
    }

    public partial class SigortaMetrics
    {
        [JsonProperty("SigortaDakikadaOrtalamaKayitAdedi")]
        public double SigortaDakikadaOrtalamaKayitAdedi { get; set; }

        [JsonProperty("SigortaDakikadaOrtalamaKayitAdediOncekiSaat")]
        public double SigortaDakikadaOrtalamaKayitAdediOncekiSaat { get; set; }

        [JsonProperty("SigortaDakikadaOrtalamaKayitAdediOncekiGun")]
        public double SigortaDakikadaOrtalamaKayitAdediOncekiGun { get; set; }

        [JsonProperty("SigortaDakikadaOrtalamaPoliceZeyilAdedi")]
        public double SigortaDakikadaOrtalamaPoliceZeyilAdedi { get; set; }

        [JsonProperty("SigortaDakikadaOrtalamaPoliceZeyilAdediOncekiSaat")]
        public double SigortaDakikadaOrtalamaPoliceZeyilAdediOncekiSaat { get; set; }

        [JsonProperty("SigortaDakikadaOrtalamaPoliceZeyilAdediOncekiGun")]
        public double SigortaDakikadaOrtalamaPoliceZeyilAdediOncekiGun { get; set; }

        [JsonProperty("SbmTrafikTeklifTalepMetrics")]
        public SbmTrafikTeklifTalepMetrics SbmTrafikTeklifTalepMetrics { get; set; }

        [JsonProperty("Notlar")]
        public object[] Notlar { get; set; }

        [JsonProperty("SbmGonderilenIstekMetrics")]
        public object[] SbmGonderilenIstekMetrics { get; set; }
    }

    public partial class SbmTrafikTeklifTalepMetrics
    {
        [JsonProperty("SonIstekBasariliYanitlanmis")]
        public bool SonIstekBasariliYanitlanmis { get; set; }

        [JsonProperty("SonBesDakikaTalepAdet")]
        public int SonBesDakikaTalepAdet { get; set; }

        [JsonProperty("SonBesDakikaBasariliTalepAdet")]
        public int SonBesDakikaBasariliTalepAdet { get; set; }

        [JsonProperty("SonBesDakikaBasarisizTalepAdet")]
        public int SonBesDakikaBasarisizTalepAdet { get; set; }
    }

    public partial class AswMetricsModel
    {
        public static AswMetricsModel FromJson(string json) => JsonConvert.DeserializeObject<AswMetricsModel>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AswMetricsModel self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
