namespace Banque.Produits.Professionnel
{
    /// <summary>
    /// Produit Concret : Attestation avec mentions légales pour professionnels
    /// </summary>
    public class AttestationProfessionnel : IAttestationCompte
    {
        public string Titulaire { get; set; }
        public string NumeroCompte { get; set; }
        public DateTime DateOuverture { get; set; }
        public string SIRET { get; set; }
        public string RaisonSociale { get; set; }

        public AttestationProfessionnel(string titulaire, string numeroCompte,
            DateTime dateOuverture, string siret, string raisonSociale)
        {
            Titulaire = titulaire;
            NumeroCompte = numeroCompte;
            DateOuverture = dateOuverture;
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
│   ATTESTATION DE COMPTE PROFESSIONNEL        │
│   (Avec mentions légales spécifiques)        │
├──────────────────────────────────────────────┤
│ Raison Sociale : {RaisonSociale,-26} │
│ SIRET          : {SIRET,-26} │
│ Représentant   : {Titulaire,-26} │
│ N° Compte      : {NumeroCompte,-26} │
│ Date ouverture : {DateOuverture:dd/MM/yyyy,-26} │
├──────────────────────────────────────────────┤
│ MENTIONS LÉGALES PROFESSIONNELLES            │
│ - Compte professionnel soumis aux CGV PRO    │
│ - Attestation valable pour démarches admin.  │
│ - Conforme aux normes ACPR et AMF            │
└──────────────────────────────────────────────┘
Document sécurisé - Usage professionnel uniquement";
        }
    }
}
