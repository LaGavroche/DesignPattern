using Assurance;

Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════╗
║     DÉMONSTRATION DU PATTERN PROTOTYPE                            ║
║     Système de Génération de Contrats d'Assurance                 ║
╚═══════════════════════════════════════════════════════════════════╝
");

// ============================================
// ÉTAPE 1 : Initialiser le registre des prototypes
// (Opération coûteuse, faite UNE SEULE FOIS)
// ============================================

var registre = new RegistreContrats();
var chrono = System.Diagnostics.Stopwatch.StartNew();

registre.Initialiser();

chrono.Stop();
Console.WriteLine($"⏱️ Temps d'initialisation: {chrono.ElapsedMilliseconds}ms\n");


// ============================================
// ÉTAPE 2 : Créer des contrats par CLONAGE (rapide !)
// ============================================

Console.WriteLine("\n═══════════════════════════════════════════════════════");
Console.WriteLine("   CRÉATION DE CONTRATS PAR CLONAGE (RAPIDE)");
Console.WriteLine("═══════════════════════════════════════════════════════\n");

chrono.Restart();

// --- Contrat Habitation pour M. Dupont ---
Console.WriteLine("📝 Création contrat habitation pour M. Dupont:");
var contratDupont = (ContratHabitation)registre.ObtenirContrat("habitation");
contratDupont.Personnaliser("Jean DUPONT", new DateTime(2024, 1, 1), 450m);
contratDupont.SetAdresse("15 rue de la Paix, 75001 Paris");
contratDupont.SetFranchise(200m);
contratDupont.ActiverOptionVol();
contratDupont.AjouterAnnexe("Annexe piscine");

// --- Contrat Habitation pour Mme Martin (variante) ---
Console.WriteLine("\n📝 Création contrat habitation pour Mme Martin:");
var contratMartin = (ContratHabitation)registre.ObtenirContrat("habitation");
contratMartin.Personnaliser("Marie MARTIN", new DateTime(2024, 2, 15), 380m);
contratMartin.SetAdresse("8 avenue Victor Hugo, 69002 Lyon");
// Franchise par défaut, pas d'option vol

// --- Contrat Automobile pour M. Dupont ---
Console.WriteLine("\n📝 Création contrat automobile pour M. Dupont:");
var autoDupont = (ContratAutomobile)registre.ObtenirContrat("automobile");
autoDupont.Personnaliser("Jean DUPONT", new DateTime(2024, 1, 1), 720m);
autoDupont.SetVehicule("AB-123-CD", "Peugeot", "308");
autoDupont.ActiverTousRisques();

// --- Contrat Vie pour Mme Martin ---
Console.WriteLine("\n📝 Création contrat vie pour Mme Martin:");
var vieMartine = (ContratVie)registre.ObtenirContrat("vie");
vieMartine.Personnaliser("Marie MARTIN", new DateTime(2024, 3, 1), 150m);
vieMartine.SetCapital(200000m);
vieMartine.SetBeneficiaire("Ses enfants");
vieMartine.ActiverOptionInvalidite();

chrono.Stop();
Console.WriteLine($"\n⏱️ Temps pour créer 4 contrats par clonage: {chrono.ElapsedMilliseconds}ms");
Console.WriteLine("   (Beaucoup plus rapide que 4 créations from scratch !)\n");


// ============================================
// ÉTAPE 3 : Afficher les contrats créés
// ============================================

Console.WriteLine("\n═══════════════════════════════════════════════════════");
Console.WriteLine("   CONTRATS GÉNÉRÉS");
Console.WriteLine("═══════════════════════════════════════════════════════");

contratDupont.Afficher();
contratMartin.Afficher();
autoDupont.Afficher();
vieMartine.Afficher();


// ============================================
// ÉTAPE 4 : Démonstration des "variations mineures"
// Un client veut plusieurs versions du même contrat
// ============================================

Console.WriteLine("\n═══════════════════════════════════════════════════════");
Console.WriteLine("   VARIATIONS D'UN MÊME CONTRAT (cas d'usage clé)");
Console.WriteLine("═══════════════════════════════════════════════════════\n");

Console.WriteLine("M. Dupont veut comparer 3 options de franchise différentes:\n");

// On clone le contrat existant de M. Dupont pour créer des variantes
var option1 = (ContratHabitation)contratDupont.Cloner();
option1.SetFranchise(100m);
option1.Personnaliser("Jean DUPONT", contratDupont.DateDebut, 520m);

var option2 = (ContratHabitation)contratDupont.Cloner();
option2.SetFranchise(300m);
option2.Personnaliser("Jean DUPONT", contratDupont.DateDebut, 420m);

var option3 = (ContratHabitation)contratDupont.Cloner();
option3.SetFranchise(500m);
option3.Personnaliser("Jean DUPONT", contratDupont.DateDebut, 350m);

Console.WriteLine($"   Option 1: Franchise {option1.Franchise}€ → Prime {option1.MontantPrime}€/an");
Console.WriteLine($"   Option 2: Franchise {option2.Franchise}€ → Prime {option2.MontantPrime}€/an");
Console.WriteLine($"   Option 3: Franchise {option3.Franchise}€ → Prime {option3.MontantPrime}€/an");


// ============================================
// ÉTAPE 5 : Ajouter un prototype personnalisé fréquent
// ============================================

Console.WriteLine("\n\n═══════════════════════════════════════════════════════");
Console.WriteLine("   AJOUT D'UN PROTOTYPE PERSONNALISÉ");
Console.WriteLine("═══════════════════════════════════════════════════════\n");

// Créer une variante "premium" qui sera souvent demandée
var habitationPremium = (ContratHabitation)registre.ObtenirContrat("habitation");
habitationPremium.SetFranchise(0m);  // Pas de franchise
habitationPremium.ActiverOptionVol();
habitationPremium.AjouterAnnexe("Annexe objets de valeur");
habitationPremium.AjouterAnnexe("Annexe responsabilité civile étendue");

// L'ajouter au registre pour réutilisation
registre.AjouterPrototype("habitation-premium", habitationPremium);

// Maintenant on peut créer des contrats premium rapidement
Console.WriteLine("\n📝 Création rapide d'un contrat premium:");
var contratPremium = (ContratHabitation)registre.ObtenirContrat("habitation-premium");
contratPremium.Personnaliser("Pierre RICHE", new DateTime(2024, 6, 1), 890m);
contratPremium.SetAdresse("1 Place Vendôme, 75001 Paris");
contratPremium.Afficher();


Console.WriteLine("\n═══════════════════════════════════════════════════════");
Console.WriteLine("   FIN DE LA DÉMONSTRATION");
Console.WriteLine("═══════════════════════════════════════════════════════");
