namespace TP3_Bridge.Implementations
{
    /// <summary>
    /// CONCRETE IMPLEMENTOR : Envoi par Discord (webhook)
    /// Démontre la facilité d'ajout d'une nouvelle plateforme
    /// </summary>
    public class DiscordPlateforme : IPlateformeEnvoi
    {
        public string NomPlateforme => "Discord";

        public void Envoyer(string titre, string contenu, string destinataire)
        {
            Console.WriteLine($"┌─────────────────────────────────────────────────┐");
            Console.WriteLine($"│ 🎮 DISCORD                                      │");
            Console.WriteLine($"│    Channel: #{destinataire,-35} │");
            Console.WriteLine($"├─────────────────────────────────────────────────┤");
            Console.WriteLine($"│ **{titre}**                                     │");
            Console.WriteLine($"│ {contenu,-46} │");
            Console.WriteLine($"│                                                 │");
            Console.WriteLine($"│ 👍 0   💬 0   🔄 0                              │");
            Console.WriteLine($"└─────────────────────────────────────────────────┘");
            Console.WriteLine();
        }
    }
}
