// Démonstration du pattern Builder + LINQ

Console.WriteLine("=== Pattern Builder ===\n");

// Création du builder
VehiculeBuilder builder = new VehiculeBuilder();

// Construction de véhicules avec chaînage de méthodes
Vehicule v1 = builder
    .SetMarque("Peugeot")
    .SetModele("308")
    .SetCouleur("Rouge")
    .SetPuissance(130)
    .AvecGPS()
    .AvecClimatisation()
    .SetPrix(25000)
    .Build();

Vehicule v2 = builder
    .SetMarque("Renault")
    .SetModele("Clio")
    .SetCouleur("Bleu")
    .SetPuissance(90)
    .AvecClimatisation()
    .SetPrix(18000)
    .Build();

Vehicule v3 = builder
    .SetMarque("BMW")
    .SetModele("Serie 3")
    .SetCouleur("Noir")
    .SetPuissance(190)
    .AvecGPS()
    .AvecClimatisation()
    .AvecToitOuvrant()
    .SetPrix(45000)
    .Build();

// Liste de véhicules
List<Vehicule> catalogue = new List<Vehicule> { v1, v2, v3 };

// Afficher tous les véhicules
foreach (var v in catalogue)
{
    v.Afficher();
    Console.WriteLine();
}

// === REQUÊTES LINQ ===
Console.WriteLine("=== Requêtes LINQ ===\n");

// 1. Véhicules avec GPS
var avecGPS = catalogue.Where(v => v.GPS);
Console.WriteLine("Véhicules avec GPS:");
foreach (var v in avecGPS)
    Console.WriteLine($"  - {v.Marque} {v.Modele}");

Console.WriteLine();

// 2. Véhicules à moins de 30000€, triés par prix
var moinsde30k = catalogue
    .Where(v => v.Prix < 30000)
    .OrderBy(v => v.Prix);
Console.WriteLine("Véhicules < 30000€ (triés par prix):");
foreach (var v in moinsde30k)
    Console.WriteLine($"  - {v.Marque} {v.Modele} : {v.Prix}€");

Console.WriteLine();

// 3. Véhicule le plus puissant
var plusPuissant = catalogue.MaxBy(v => v.Puissance);
Console.WriteLine($"Plus puissant: {plusPuissant?.Marque} {plusPuissant?.Modele} ({plusPuissant?.Puissance} CV)");

// 4. Prix moyen
var prixMoyen = catalogue.Average(v => v.Prix);
Console.WriteLine($"Prix moyen: {prixMoyen}€");
