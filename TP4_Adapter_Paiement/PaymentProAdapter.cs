namespace TP4_Adapter_Paiement;

/// <summary>
/// ADAPTER - Fait le pont entre IPaymentService et PaymentPro.
///
/// Pattern : Object Adapter (utilise la composition)
/// - Implémente l'interface cible (IPaymentService)
/// - Contient une référence vers l'adaptee (PaymentPro)
/// - Traduit les appels entre les deux interfaces
/// </summary>
public class PaymentProAdapter : IPaymentService
{
    // ===== COMPOSITION : l'adapter contient l'adaptee =====
    private readonly PaymentPro _paymentPro;

    /// <summary>
    /// Constructeur - On injecte l'instance de PaymentPro à adapter
    /// </summary>
    public PaymentProAdapter(PaymentPro paymentPro)
    {
        _paymentPro = paymentPro ?? throw new ArgumentNullException(nameof(paymentPro));
    }

    // ===== MÉTHODES DE CONVERSION (privées) =====

    /// <summary>
    /// Convertit le code devise string vers le code numérique de PaymentPro
    /// "EUR" -> 1, "USD" -> 2, "GBP" -> 3
    /// </summary>
    private int ConvertCurrencyToCode(string currency)
    {
        return currency.ToUpper() switch
        {
            "EUR" => 1,
            "USD" => 2,
            "GBP" => 3,
            _ => throw new ArgumentException($"Devise non supportée: {currency}")
        };
    }

    /// <summary>
    /// Convertit le code statut numérique de PaymentPro vers une chaîne
    /// 0 -> "Pending", 1 -> "Completed", 2 -> "Failed"
    /// </summary>
    private string ConvertStatusToString(int status)
    {
        return status switch
        {
            0 => "Pending",
            1 => "Completed",
            2 => "Failed",
            _ => "Unknown"
        };
    }

    // ===== IMPLÉMENTATION DE IPaymentService =====

    public bool ProcessPayment(decimal amount, string currency)
    {
        // 1. Convertir decimal -> double
        double montant = (double)amount;

        // 2. Convertir string -> code devise
        int codeDevise = ConvertCurrencyToCode(currency);

        // 3. Déléguer à PaymentPro
        string reference = _paymentPro.ExecuterTransaction(montant, codeDevise);

        // 4. Interpréter le résultat
        return !string.IsNullOrEmpty(reference);
    }

    public bool RefundPayment(string transactionId, decimal amount)
    {
        // PaymentPro annule complètement la transaction
        // (pas de remboursement partiel supporté)
        return _paymentPro.AnnulerTransaction(transactionId);
    }

    public string GetTransactionStatus(string transactionId)
    {
        // 1. Récupérer le code numérique
        int statusCode = _paymentPro.ObtenirEtat(transactionId);

        // 2. Convertir en chaîne descriptive
        return ConvertStatusToString(statusCode);
    }
}
