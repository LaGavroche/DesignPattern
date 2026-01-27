namespace Banque.Produits
{
    /// <summary>
    /// Produit Abstrait : Interface pour l'Attestation de Compte
    /// Définit le contrat que toutes les attestations doivent respecter
    /// </summary>
    public interface IAttestationCompte
    {
        string Titulaire { get; set; }
        string NumeroCompte { get; set; }
        DateTime DateOuverture { get; set; }

        string Generer();
        void AfficherLogo();
    }
}
