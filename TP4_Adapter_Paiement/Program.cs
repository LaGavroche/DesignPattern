namespace TP4_Adapter_Paiement;

class Program
{
    /// <summary>
    /// Méthode client existante - NE CONNAIT QUE IPaymentService
    /// Grâce au polymorphisme, elle peut utiliser n'importe quelle implémentation
    /// </summary>
    static void ProcessOrder(IPaymentService paymentService, decimal total)
    {
        Console.WriteLine($"Traitement d'une commande de {total} EUR...");

        bool success = paymentService.ProcessPayment(total, "EUR");

        if (success)
        {
            Console.WriteLine("Commande traitée avec succès !");
        }
        else
        {
            Console.WriteLine("Échec du traitement de la commande.");
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║          TP4 - Pattern Adapter : Paiements               ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.WriteLine();

        // ===== TEST 1 : Service interne (existant) =====
        Console.WriteLine("═══ Test 1 : InternalPaymentService ═══");
        IPaymentService internalService = new InternalPaymentService();
        ProcessOrder(internalService, 99.99m);

        Console.WriteLine();

        // ===== TEST 2 : PaymentPro via l'Adapter =====
        Console.WriteLine("═══ Test 2 : PaymentPro via Adapter ═══");

        // Étape 1 : Créer l'instance de PaymentPro (service externe)
        PaymentPro paymentPro = new PaymentPro();

        // Étape 2 : L'envelopper dans notre Adapter
        IPaymentService adaptedService = new PaymentProAdapter(paymentPro);

        // Étape 3 : Utiliser exactement comme InternalPaymentService !
        // ProcessOrder ne voit AUCUNE différence
        ProcessOrder(adaptedService, 149.99m);

        Console.WriteLine();

        // ===== TEST 3 : Autres méthodes =====
        Console.WriteLine("═══ Test 3 : Autres méthodes de l'Adapter ═══");

        // Test GetTransactionStatus
        string status = adaptedService.GetTransactionStatus("TX-12345");
        Console.WriteLine($"Statut de TX-12345 : {status}");

        // Test RefundPayment
        bool refunded = adaptedService.RefundPayment("TX-12345", 50.00m);
        Console.WriteLine($"Remboursement effectué : {refunded}");

        Console.WriteLine();

        // ===== TEST 4 : Paiement en USD =====
        Console.WriteLine("═══ Test 4 : Paiement en USD ═══");
        adaptedService.ProcessPayment(199.99m, "USD");

        Console.WriteLine();

        // ===== TEST 5 : QuickPay via son Adapter =====
        Console.WriteLine("═══ Test 5 : QuickPay via Adapter ═══");

        // Même principe : créer le service, l'envelopper, l'utiliser
        QuickPay quickPay = new QuickPay();
        IPaymentService quickPayService = new QuickPayAdapter(quickPay);

        // ProcessOrder fonctionne EXACTEMENT pareil !
        ProcessOrder(quickPayService, 299.99m);

        Console.WriteLine();
        Console.WriteLine("═══ Fin des tests ═══");
    }
}
