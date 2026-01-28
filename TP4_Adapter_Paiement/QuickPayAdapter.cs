namespace TP4_Adapter_Paiement;

/// <summary>
/// NOUVEL ADAPTER pour QuickPay.
///
/// Même principe que PaymentProAdapter :
/// - Implémente IPaymentService (interface cible)
/// - Contient une référence vers QuickPay (adaptee)
/// - Traduit les appels entre les deux interfaces
/// </summary>
public class QuickPayAdapter : IPaymentService
{
    private readonly QuickPay _quickPay;

    public QuickPayAdapter(QuickPay quickPay)
    {
        _quickPay = quickPay ?? throw new ArgumentNullException(nameof(quickPay));
    }

    // ===== MÉTHODES DE CONVERSION =====

    /// <summary>
    /// Convertit decimal en centimes (int)
    /// 99.99€ -> 9999 centimes
    /// </summary>
    private int ConvertToCents(decimal amount)
    {
        return (int)(amount * 100);
    }

    /// <summary>
    /// Convertit string vers l'enum Currency de QuickPay
    /// </summary>
    private QuickPay.Currency ConvertCurrency(string currency)
    {
        return currency.ToUpper() switch
        {
            "EUR" => QuickPay.Currency.Euro,
            "USD" => QuickPay.Currency.Dollar,
            "GBP" => QuickPay.Currency.Pound,
            _ => throw new ArgumentException($"Devise non supportée: {currency}")
        };
    }

    /// <summary>
    /// Convertit le statut QuickPay vers notre format standard
    /// </summary>
    private string ConvertStatus(string quickPayState)
    {
        return quickPayState switch
        {
            "PROCESSING" => "Pending",
            "DONE" => "Completed",
            "ERROR" => "Failed",
            _ => "Unknown"
        };
    }

    // ===== IMPLÉMENTATION DE IPaymentService =====

    public bool ProcessPayment(decimal amount, string currency)
    {
        // Conversions
        int cents = ConvertToCents(amount);
        QuickPay.Currency curr = ConvertCurrency(currency);

        // Délégation
        QuickPay.PaymentResult result = _quickPay.Pay(cents, curr);

        return result.Success;
    }

    public bool RefundPayment(string transactionId, decimal amount)
    {
        return _quickPay.CancelPayment(transactionId);
    }

    public string GetTransactionStatus(string transactionId)
    {
        QuickPay.StatusResult result = _quickPay.CheckStatus(transactionId);
        return ConvertStatus(result.State);
    }
}
