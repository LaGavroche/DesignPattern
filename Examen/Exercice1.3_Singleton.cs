using System;

public sealed class ConfigurationHospitaliere
{
    private static readonly Lazy<ConfigurationHospitaliere> _instance =
        new Lazy<ConfigurationHospitaliere>(() => new ConfigurationHospitaliere());

    public string ConnectionString { get; set; }
    public string Langue { get; set; }
    public string FuseauHoraire { get; set; }

    private ConfigurationHospitaliere()
    {
        ConnectionString = "Server=localhost;Database=HopitalDB;";
        Langue = "fr-FR";
        FuseauHoraire = "Europe/Paris";
    }

    public static ConfigurationHospitaliere GetInstance() => _instance.Value;
}

class Program
{
    static void Main()
    {
        var config1 = ConfigurationHospitaliere.GetInstance();
        var config2 = ConfigurationHospitaliere.GetInstance();

        config1.Langue = "en-US";

        Console.WriteLine(config2.Langue);        // "en-US" → même instance
        Console.WriteLine(config1 == config2);     // True
    }
}
