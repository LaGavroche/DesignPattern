namespace TP4_Adapter_Paiement;

/// <summary>
/// ADAPTEE - Service externe avec une interface incompatible.
/// NE PAS MODIFIER - C'est une bibliothèque externe.
///
/// Différences avec IPaymentService :
/// - Utilise double au lieu de decimal
/// - Utilise des codes numériques pour les devises (1=EUR, 2=USD, 3=GBP)
/// - Retourne des codes numériques pour les statuts (0=En cours, 1=Validé, 2=Échoué)
/// - Noms de méthodes en français
/// </summary>
public class PaymentPro
{
    public string ExecuterTransaction(double montant, int codeDevise)
    {
        Console.WriteLine($"[PaymentPro] Transaction de {montant} avec devise code {codeDevise}");
        return Guid.NewGuid().ToString();
    }

    public bool AnnulerTransaction(string reference)
    {
        Console.WriteLine($"[PaymentPro] Annulation de {reference}");
        return true;
    }

    public int ObtenirEtat(string reference)
    {
        // 0 = En cours, 1 = Validé, 2 = Échoué
        return 1;
    }
}
