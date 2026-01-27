using Banque.Produits;
using Banque.Produits.Particulier;

namespace Banque.Fabriques
{
    /// <summary>
    /// FABRIQUE CONCRÈTE pour les clients PARTICULIERS
    /// Produit une famille cohérente de documents : RIB simplifié + Attestation standard
    /// </summary>
    public class ParticulierDocumentFactory : IDocumentBancaireFactory
    {
        private readonly string _nomClient;
        private readonly string _iban;
        private readonly string _bic;
        private readonly string _numeroCompte;
        private readonly DateTime _dateOuverture;

        public ParticulierDocumentFactory(string nomClient, string iban, string bic,
            string numeroCompte, DateTime dateOuverture)
        {
            _nomClient = nomClient;
            _iban = iban;
            _bic = bic;
            _numeroCompte = numeroCompte;
            _dateOuverture = dateOuverture;
        }

        /// <summary>
        /// Crée un RIB simplifié pour particulier
        /// </summary>
        public IReleveIdentiteBancaire CreerRIB()
        {
            return new RIBParticulier(_nomClient, _iban, _bic);
        }

        /// <summary>
        /// Crée une attestation standardisée pour particulier
        /// </summary>
        public IAttestationCompte CreerAttestation()
        {
            return new AttestationParticulier(_nomClient, _numeroCompte, _dateOuverture);
        }
    }
}
