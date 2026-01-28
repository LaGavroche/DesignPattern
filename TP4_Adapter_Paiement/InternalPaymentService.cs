namespace TP4_Adapter_Paiement;

/// <summary>
/// Service de paiement interne existant.
/// Implémente déjà IPaymentService - fonctionne correctement.
/// </summary>
public class InternalPaymentService : IPaymentService
{
    public bool ProcessPayment(decimal amount, string currency)
    {
        Console.WriteLine($"[InternalPayment] Paiement de {amount} {currency}");
        return true;
    }

    public bool RefundPayment(string transactionId, decimal amount)
    {
        Console.WriteLine($"[InternalPayment] Remboursement {transactionId} - {amount}");
        return true;
    }

    public string GetTransactionStatus(string transactionId)
    {
        return "Completed";
    }
}
