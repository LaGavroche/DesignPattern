namespace Banque.Produits.Professionnel
{
    /// <summary>
    /// Produit Concret : RIB détaillé avec SIRET pour les professionnels
    /// </summary>
    public class RIBProfessionnel : IReleveIdentiteBancaire
    {
        public string Titulaire { get; set; }
        public string IBAN { get; set; }
        public string BIC { get; set; }
        public string SIRET { get; set; }
        public string RaisonSociale { get; set; }

        public RIBProfessionnel(string titulaire, string iban, string bic, string siret, string raisonSociale)
        {
            Titulaire = titulaire;
            IBAN = iban;
            BIC = bic;
            SIRET = siret;
            RaisonSociale = raisonSociale;
        }

        public void AfficherLogo()
        {
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║       BANQUE CENTRALE [LOGO]                 ║");
            Console.WriteLine("║       Division Entreprises                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
        }

        public string Generer()
        {
            AfficherLogo();
            return $@"
┌──────────────────────────────────────────────┐
│   RELEVÉ D'IDENTITÉ BANCAIRE                 │
│   (Version Professionnelle - Détaillé)       │
├──────────────────────────────────────────────┤
│ Raison Sociale : {RaisonSociale,-26} │
│ Représentant   : {Titulaire,-26} │
│ SIRET          : {SIRET,-26} │
├──────────────────────────────────────────────┤
│ IBAN           : {IBAN,-26} │
│ BIC            : {BIC,-26} │
├──────────────────────────────────────────────┤
│ Code APE - TVA Intracommunautaire inclus     │
└──────────────────────────────────────────────┘
Document sécurisé - Usage professionnel";
        }
    }
}
