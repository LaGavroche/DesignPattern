using Banque;
using Banque.Fabriques;

Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
Console.WriteLine("║     DÉMONSTRATION DU PATTERN ABSTRACT FACTORY                  ║");
Console.WriteLine("║     Système de Production de Documents Bancaires               ║");
Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");

// ============================================
// EXEMPLE 1 : Client PARTICULIER
// ============================================
Console.WriteLine("\n\n>>> TRAITEMENT CLIENT PARTICULIER <<<\n");

// Création de la fabrique concrète pour particulier
IDocumentBancaireFactory fabriqueParticulier = new ParticulierDocumentFactory(
    nomClient: "Jean DUPONT",
    iban: "FR76 1234 5678 9012 3456 7890 123",
    bic: "BNPAFRPP",
    numeroCompte: "00012345678",
    dateOuverture: new DateTime(2020, 3, 15)
);

// Le service utilise la fabrique abstraite (il ne sait pas que c'est un particulier)
ServiceDocuments serviceParticulier = new ServiceDocuments(fabriqueParticulier);
serviceParticulier.GenererTousLesDocuments();


// ============================================
// EXEMPLE 2 : Client PROFESSIONNEL
// ============================================
Console.WriteLine("\n\n>>> TRAITEMENT CLIENT PROFESSIONNEL <<<\n");

// Création de la fabrique concrète pour professionnel
IDocumentBancaireFactory fabriqueProfessionnel = new ProfessionnelDocumentFactory(
    representant: "Marie MARTIN",
    raisonSociale: "TECH SOLUTIONS SAS",
    siret: "123 456 789 00012",
    iban: "FR76 9876 5432 1098 7654 3210 987",
    bic: "AGRIFRPP",
    numeroCompte: "00098765432",
    dateOuverture: new DateTime(2019, 7, 1)
);

// Même service, fabrique différente → documents différents
ServiceDocuments serviceProfessionnel = new ServiceDocuments(fabriqueProfessionnel);
serviceProfessionnel.GenererTousLesDocuments();


// ============================================
// Démonstration de l'interchangeabilité
// ============================================
Console.WriteLine("\n\n>>> DÉMONSTRATION DE L'INTERCHANGEABILITÉ <<<");
Console.WriteLine("Le même code (GenererDocuments) produit des résultats différents");
Console.WriteLine("selon la fabrique injectée, sans modification du ServiceDocuments !");

void GenererDocuments(IDocumentBancaireFactory factory, string typeClient)
{
    Console.WriteLine($"\n--- Documents pour {typeClient} ---");
    var service = new ServiceDocuments(factory);
    Console.WriteLine(service.GenererRIB());
}

GenererDocuments(fabriqueParticulier, "Particulier");
GenererDocuments(fabriqueProfessionnel, "Professionnel");

Console.WriteLine("\n\nFin de la démonstration.");
