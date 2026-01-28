namespace TP4_Adapter_Paiement;

/// <summary>
/// NOUVEL ADAPTEE - Autre service externe avec sa propre interface.
/// NE PAS MODIFIER - C'est une bibliothèque externe.
///
/// Différences avec IPaymentService ET PaymentPro :
/// - Montant en centimes (int) au lieu de decimal ou double
/// - Devise en enum au lieu de string ou int
/// - Méthodes avec des noms différents
/// - Retourne des objets au lieu de types primitifs
/// </summary>
public class QuickPay
{
    public enum Currency { Euro, Dollar, Pound }

    public class PaymentResult
    {
        public bool Success { get; set; }
        public string TransactionRef { get; set; } = "";
    }

    public class StatusResult
    {
        public string State { get; set; } = "";  // "PROCESSING", "DONE", "ERROR"
    }

    // Montant en CENTIMES (int), pas en euros
    public PaymentResult Pay(int amountInCents, Currency currency)
    {
        Console.WriteLine($"[QuickPay] Paiement de {amountInCents} centimes en {currency}");
        return new PaymentResult
        {
            Success = true,
            TransactionRef = $"QP-{Guid.NewGuid().ToString()[..8]}"
        };
    }

    public bool CancelPayment(string reference)
    {
        Console.WriteLine($"[QuickPay] Annulation de {reference}");
        return true;
    }

    public StatusResult CheckStatus(string reference)
    {
        return new StatusResult { State = "DONE" };
    }
}
