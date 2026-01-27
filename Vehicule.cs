// Le produit à construire
public class Vehicule
{
    public string Marque { get; set; } = "";
    public string Modele { get; set; } = "";
    public string Couleur { get; set; } = "";
    public int Puissance { get; set; }
    public bool GPS { get; set; }
    public bool Climatisation { get; set; }
    public bool ToitOuvrant { get; set; }
    public decimal Prix { get; set; }

    public void Afficher()
    {
        Console.WriteLine($"Véhicule: {Marque} {Modele}");
        Console.WriteLine($"  Couleur: {Couleur}");
        Console.WriteLine($"  Puissance: {Puissance} CV");
        Console.WriteLine($"  Options: GPS={GPS}, Clim={Climatisation}, Toit={ToitOuvrant}");
        Console.WriteLine($"  Prix: {Prix}€");
    }
}
