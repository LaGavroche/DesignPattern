using Banque.Fabriques;
using Banque.Produits;

namespace Banque
{
    /// <summary>
    /// CLIENT du pattern Abstract Factory
    /// Cette classe utilise la fabrique abstraite sans connaître les implémentations concrètes.
    /// Elle peut générer des documents pour n'importe quel type de client.
    /// </summary>
    public class ServiceDocuments
    {
        private readonly IDocumentBancaireFactory _factory;

        /// <summary>
        /// Injection de dépendance : le service reçoit une fabrique abstraite
        /// Il ne sait pas s'il s'agit d'une fabrique Particulier ou Professionnel
        /// </summary>
        public ServiceDocuments(IDocumentBancaireFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Génère tous les documents pour le client
        /// Utilise les méthodes de la fabrique abstraite
        /// </summary>
        public void GenererTousLesDocuments()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("  GÉNÉRATION DES DOCUMENTS BANCAIRES");
            Console.WriteLine(new string('=', 50));

            // Création du RIB via la fabrique (sans connaître le type concret)
            IReleveIdentiteBancaire rib = _factory.CreerRIB();
            Console.WriteLine(rib.Generer());

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            // Création de l'attestation via la fabrique
            IAttestationCompte attestation = _factory.CreerAttestation();
            Console.WriteLine(attestation.Generer());

            Console.WriteLine("\n" + new string('=', 50));
        }

        /// <summary>
        /// Génère uniquement le RIB
        /// </summary>
        public string GenererRIB()
        {
            IReleveIdentiteBancaire rib = _factory.CreerRIB();
            return rib.Generer();
        }

        /// <summary>
        /// Génère uniquement l'attestation
        /// </summary>
        public string GenererAttestation()
        {
            IAttestationCompte attestation = _factory.CreerAttestation();
            return attestation.Generer();
        }
    }
}
