namespace TP3_Bridge.Implementations
{
    /// <summary>
    /// CONCRETE IMPLEMENTOR : Envoi par Push notification
    /// </summary>
    public class PushPlateforme : IPlateformeEnvoi
    {
        public string NomPlateforme => "Push";

        public void Envoyer(string titre, string contenu, string destinataire)
        {
            Console.WriteLine($"┌─────────────────────────────────────────────┐");
            Console.WriteLine($"│ 🔔 PUSH NOTIFICATION                        │");
            Console.WriteLine($"│   Device: {destinataire,-32} │");
            Console.WriteLine($"├─────────────────────────────────────────────┤");
            Console.WriteLine($"│ ▸ {titre,-40} │");
            Console.WriteLine($"│   {contenu,-40} │");
            Console.WriteLine($"└─────────────────────────────────────────────┘");
            Console.WriteLine();
        }
    }
}
