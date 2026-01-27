namespace TP3_Bridge.Implementations
{
    /// <summary>
    /// CONCRETE IMPLEMENTOR : Envoi par Email
    /// </summary>
    public class EmailPlateforme : IPlateformeEnvoi
    {
        public string NomPlateforme => "Email";

        public void Envoyer(string titre, string contenu, string destinataire)
        {
            Console.WriteLine($"╔══════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  📧 EMAIL                                            ║");
            Console.WriteLine($"╠══════════════════════════════════════════════════════╣");
            Console.WriteLine($"║  À      : {destinataire,-40} ║");
            Console.WriteLine($"║  Sujet  : {titre,-40} ║");
            Console.WriteLine($"╠══════════════════════════════════════════════════════╣");
            Console.WriteLine($"║  {contenu,-50} ║");
            Console.WriteLine($"╚══════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }
    }
}
