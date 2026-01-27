using Banque.Produits;

namespace Banque.Fabriques
{
    /// <summary>
    /// FABRIQUE ABSTRAITE
    /// Définit l'interface de création pour une famille de documents bancaires.
    /// Chaque méthode retourne un type abstrait (interface).
    /// Le client utilise cette interface sans connaître les classes concrètes.
    /// </summary>
    public interface IDocumentBancaireFactory
    {
        /// <summary>
        /// Crée un Relevé d'Identité Bancaire selon le type de client
        /// </summary>
        IReleveIdentiteBancaire CreerRIB();

        /// <summary>
        /// Crée une Attestation de Compte selon le type de client
        /// </summary>
        IAttestationCompte CreerAttestation();
    }
}
