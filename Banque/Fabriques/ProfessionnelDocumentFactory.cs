using Banque.Produits;
using Banque.Produits.Professionnel;

namespace Banque.Fabriques
{
    /// <summary>
    /// FABRIQUE CONCRÈTE pour les clients PROFESSIONNELS
    /// Produit une famille cohérente de documents : RIB détaillé + Attestation avec mentions légales
    /// </summary>
    public class ProfessionnelDocumentFactory : IDocumentBancaireFactory
    {
        private readonly string _representant;
        private readonly string _raisonSociale;
        private readonly string _siret;
        private readonly string _iban;
        private readonly string _bic;
        private readonly string _numeroCompte;
        private readonly DateTime _dateOuverture;

        public ProfessionnelDocumentFactory(string representant, string raisonSociale,
            string siret, string iban, string bic, string numeroCompte, DateTime dateOuverture)
        {
            _representant = representant;
            _raisonSociale = raisonSociale;
            _siret = siret;
            _iban = iban;
            _bic = bic;
            _numeroCompte = numeroCompte;
            _dateOuverture = dateOuverture;
        }

        /// <summary>
        /// Crée un RIB détaillé avec SIRET pour professionnel
        /// </summary>
        public IReleveIdentiteBancaire CreerRIB()
        {
            return new RIBProfessionnel(_representant, _iban, _bic, _siret, _raisonSociale);
        }

        /// <summary>
        /// Crée une attestation avec mentions légales pour professionnel
        /// </summary>
        public IAttestationCompte CreerAttestation()
        {
            return new AttestationProfessionnel(
                _representant, _numeroCompte, _dateOuverture, _siret, _raisonSociale);
        }
    }
}
