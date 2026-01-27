namespace Banque.Produits.Particulier
{
    /// <summary>
    /// Produit Concret : Attestation de compte standardisée pour particuliers
    /// </summary>
    public class AttestationParticulier : IAttestationCompte
    {
        public string Titulaire { get; set; }
        public string NumeroCompte { get; set; }
        public DateTime DateOuverture { get; set; }

        public AttestationParticulier(string titulaire, string numeroCompte, DateTime dateOuverture)
        {
            Titulaire = titulaire;
            NumeroCompte = numeroCompte;
            DateOuverture = dateOuverture;
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
│   ATTESTATION DE COMPTE              │
│   (Version Particulier - Standard)   │
├──────────────────────────────────────┤
│ Titulaire     : {Titulaire,-20} │
│ N° Compte     : {NumeroCompte,-20} │
│ Date ouverture: {DateOuverture:dd/MM/yyyy,-20} │
├──────────────────────────────────────┤
│ La banque atteste que le client      │
│ ci-dessus est titulaire d'un compte. │
└──────────────────────────────────────┘
Document sécurisé - Usage personnel";
        }
    }
}
