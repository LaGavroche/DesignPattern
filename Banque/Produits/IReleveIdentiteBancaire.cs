namespace Banque.Produits
{
    /// <summary>
    /// Produit Abstrait : Interface pour le Relevé d'Identité Bancaire
    /// Définit le contrat que tous les RIB doivent respecter
    /// </summary>
    public interface IReleveIdentiteBancaire
    {
        string Titulaire { get; set; }
        string IBAN { get; set; }
        string BIC { get; set; }

        string Generer();
        void AfficherLogo();
    }
}
