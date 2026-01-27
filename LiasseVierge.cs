// Pattern Singleton - Une seule instance de LiasseVierge
public class LiasseVierge
{
    // Instance unique (statique et privée)
    private static LiasseVierge? _instance;

    // Documents référencés par la liasse vierge
    public string CertificatCession { get; } = "Certificat de Cession (vierge)";
    public string DemandeImmatriculation { get; } = "Demande d'Immatriculation (vierge)";
    public string BonCommande { get; } = "Bon de Commande (vierge)";

    // Constructeur privé - empêche l'instanciation externe
    private LiasseVierge()
    {
    }

    // Point d'accès global à l'instance unique
    public static LiasseVierge GetInstance()
    {
        if (_instance == null)
        {
            _instance = new LiasseVierge();
        }
        return _instance;
    }

    public void AfficherDocuments()
    {
        Console.WriteLine("=== Liasse Vierge ===");
        Console.WriteLine("- " + CertificatCession);
        Console.WriteLine("- " + DemandeImmatriculation);
        Console.WriteLine("- " + BonCommande);
    }
}
