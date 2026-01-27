namespace TP3_Bridge.Implementations
{
    /// <summary>
    /// CONCRETE IMPLEMENTOR : Envoi par SMS
    /// </summary>
    public class SMSPlateforme : IPlateformeEnvoi
    {
        public string NomPlateforme => "SMS";

        public void Envoyer(string titre, string contenu, string destinataire)
        {
            // Les SMS sont plus courts, on combine titre et contenu
            string messageComplet = $"{titre}: {contenu}";

            // Limite à 160 caractères pour un SMS
            if (messageComplet.Length > 160)
            {
                messageComplet = messageComplet.Substring(0, 157) + "...";
            }

            Console.WriteLine($"┌────────────────────────────────────────┐");
            Console.WriteLine($"│ 📱 SMS → {destinataire,-28} │");
            Console.WriteLine($"├────────────────────────────────────────┤");
            Console.WriteLine($"│ {messageComplet,-38} │");
            Console.WriteLine($"└────────────────────────────────────────┘");
            Console.WriteLine();
        }
    }
}
