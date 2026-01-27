namespace Banque.Produits.Particulier
{
    /// <summary>
    /// Produit Concret : RIB simplifié pour les particuliers
    /// </summary>
    public class RIBParticulier : IReleveIdentiteBancaire
    {
        public string Titulaire { get; set; }
        public string IBAN { get; set; }
        public string BIC { get; set; }

        public RIBParticulier(string titulaire, string iban, string bic)
        {
            Titulaire = titulaire;
            IBAN = iban;
            BIC = bic;
        }

        public void AfficherLogo()
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║       BANQUE CENTRALE [LOGO]         ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
        }

        public string Generer()
        {
            AfficherLogo();
            return $@"
┌──────────────────────────────────────┐
│   RELEVÉ D'IDENTITÉ BANCAIRE         │
│   (Version Particulier - Simplifié)  │
├──────────────────────────────────────┤
│ Titulaire : {Titulaire,-24} │
│ IBAN      : {IBAN,-24} │
│ BIC       : {BIC,-24} │
└──────────────────────────────────────┘
Document sécurisé - Usage personnel";
        }
    }
}
