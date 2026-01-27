using TP3_Bridge.Abstractions;
using TP3_Bridge.Implementations;

Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════════════╗
║                       TP3 - PATTERN BRIDGE                                    ║
║                 Système de Notification Multi-plateforme                      ║
╚═══════════════════════════════════════════════════════════════════════════════╝
");

// ═══════════════════════════════════════════════════════════════════════════════
// DÉMONSTRATION 1 : Même notification, différentes plateformes
// ═══════════════════════════════════════════════════════════════════════════════

Console.WriteLine("═══════════════════════════════════════════════════════════════════");
Console.WriteLine("  1. MÊME NOTIFICATION → DIFFÉRENTES PLATEFORMES");
Console.WriteLine("═══════════════════════════════════════════════════════════════════\n");

// Créer les plateformes (implémentations)
var email = new EmailPlateforme();
var sms = new SMSPlateforme();
var push = new PushPlateforme();

// Notification commande via EMAIL
var commandeEmail = new NotificationCommande(email) { NumeroCommande = "12345" };
commandeEmail.EnvoyerConfirmation("client@email.com");

// Même notification via SMS
var commandeSms = new NotificationCommande(sms) { NumeroCommande = "12345" };
commandeSms.EnvoyerConfirmation("+33612345678");

// Même notification via PUSH
var commandePush = new NotificationCommande(push) { NumeroCommande = "12345" };
commandePush.EnvoyerConfirmation("device_token_abc123");


// ═══════════════════════════════════════════════════════════════════════════════
// DÉMONSTRATION 2 : Différentes notifications, même plateforme
// ═══════════════════════════════════════════════════════════════════════════════

Console.WriteLine("\n═══════════════════════════════════════════════════════════════════");
Console.WriteLine("  2. DIFFÉRENTES NOTIFICATIONS → MÊME PLATEFORME (SMS)");
Console.WriteLine("═══════════════════════════════════════════════════════════════════\n");

// Notification Livraison
var livraison = new NotificationLivraison(sms)
{
    NumeroSuivi = "TRACK789",
    Transporteur = "Chronopost"
};
livraison.EnvoyerEnCoursLivraison("+33612345678", "14h-16h");

// Notification Support
var support = new NotificationSupport(sms)
{
    NumeroTicket = "SUP-456",
    NomAgent = "Marie"
};
support.EnvoyerReponseAgent("+33612345678", "Votre problème a été identifié");


// ═══════════════════════════════════════════════════════════════════════════════
// DÉMONSTRATION 3 : Changement de plateforme dynamique
// ═══════════════════════════════════════════════════════════════════════════════

Console.WriteLine("\n═══════════════════════════════════════════════════════════════════");
Console.WriteLine("  3. CHANGEMENT DE PLATEFORME DYNAMIQUE");
Console.WriteLine("═══════════════════════════════════════════════════════════════════\n");

var notification = new NotificationCommande(email) { NumeroCommande = "99999" };

Console.WriteLine("Premier envoi via Email:");
notification.Envoyer("Commande reçue", "user@mail.com");

Console.WriteLine("Changement vers SMS:");
notification.ChangerPlateforme(sms);
notification.Envoyer("Commande expédiée", "+33600000000");

Console.WriteLine("Changement vers Push:");
notification.ChangerPlateforme(push);
notification.Envoyer("Commande livrée", "device_xyz");


// ═══════════════════════════════════════════════════════════════════════════════
// DÉMONSTRATION 4 : Ajout facile d'une nouvelle plateforme (Discord)
// ═══════════════════════════════════════════════════════════════════════════════

Console.WriteLine("\n═══════════════════════════════════════════════════════════════════");
Console.WriteLine("  4. NOUVELLE PLATEFORME : DISCORD");
Console.WriteLine("     (Ajoutée sans modifier les notifications existantes !)");
Console.WriteLine("═══════════════════════════════════════════════════════════════════\n");

var discord = new DiscordPlateforme();

// Toutes les notifications existantes fonctionnent avec Discord !
var commandeDiscord = new NotificationCommande(discord) { NumeroCommande = "77777" };
commandeDiscord.EnvoyerConfirmation("notifications");

var livraisonDiscord = new NotificationLivraison(discord)
{
    NumeroSuivi = "DIS123",
    Transporteur = "UPS"
};
livraisonDiscord.EnvoyerLivre("livraisons", "Point relais");


// ═══════════════════════════════════════════════════════════════════════════════
// DÉMONSTRATION 5 : Ajout facile d'un nouveau type (Promotion)
// ═══════════════════════════════════════════════════════════════════════════════

Console.WriteLine("\n═══════════════════════════════════════════════════════════════════");
Console.WriteLine("  5. NOUVEAU TYPE : NOTIFICATION PROMOTION");
Console.WriteLine("     (Fonctionne avec toutes les plateformes existantes !)");
Console.WriteLine("═══════════════════════════════════════════════════════════════════\n");

var promo = new NotificationPromotion(email)
{
    CodePromo = "SOLDES25",
    PourcentageReduction = 25,
    DateExpiration = DateTime.Now.AddDays(3)
};
promo.EnvoyerNouvellePromo("newsletter@client.com");

// La même promo via SMS
promo.ChangerPlateforme(sms);
promo.EnvoyerRappelExpiration("+33612345678");

// Et via Discord !
promo.ChangerPlateforme(discord);
promo.EnvoyerNouvellePromo("promotions");


// ═══════════════════════════════════════════════════════════════════════════════
// RÉSUMÉ DES AVANTAGES
// ═══════════════════════════════════════════════════════════════════════════════

Console.WriteLine(@"
═══════════════════════════════════════════════════════════════════════════════
  RÉSUMÉ : AVANTAGES DU PATTERN BRIDGE
═══════════════════════════════════════════════════════════════════════════════

  AVANT (code existant)              APRÈS (avec Bridge)
  ─────────────────────              ─────────────────────
  3 types × 3 plateformes            4 types + 4 plateformes
  = 9 méthodes dupliquées            = 8 classes indépendantes

  Ajouter Promotion + Discord:       Ajouter Promotion + Discord:
  → 4 × 4 = 16 méthodes !            → +1 classe type + 1 classe plateforme

  ✅ Pas de duplication de code
  ✅ Abstraction et implémentation évoluent indépendamment
  ✅ Respect du principe Open/Closed
  ✅ Changement de plateforme à l'exécution

═══════════════════════════════════════════════════════════════════════════════
");
