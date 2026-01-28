namespace TP4_Adapter_Paiement;

/// <summary>
/// Interface TARGET (Cible) - Utilisée par tout le système existant.
/// C'est l'interface que le client connaît et utilise.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Traite un paiement
    /// </summary>
    /// <param name="amount">Montant en decimal</param>
    /// <param name="currency">Code devise (EUR, USD, GBP)</param>
    /// <returns>True si le paiement a réussi</returns>
    bool ProcessPayment(decimal amount, string currency);

    /// <summary>
    /// Rembourse un paiement
    /// </summary>
    /// <param name="transactionId">Identifiant de la transaction</param>
    /// <param name="amount">Montant à rembourser</param>
    /// <returns>True si le remboursement a réussi</returns>
    bool RefundPayment(string transactionId, decimal amount);

    /// <summary>
    /// Récupère le statut d'une transaction
    /// </summary>
    /// <param name="transactionId">Identifiant de la transaction</param>
    /// <returns>Statut sous forme de chaîne (Pending, Completed, Failed)</returns>
    string GetTransactionStatus(string transactionId);
}
