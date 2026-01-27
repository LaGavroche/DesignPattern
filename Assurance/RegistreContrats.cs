namespace Assurance
{
    /// <summary>
    /// Registre des prototypes (Prototype Registry)
    /// Stocke les modèles de contrats pré-chargés pour clonage rapide
    /// </summary>
    public class RegistreContrats
    {
        private Dictionary<string, IContratPrototype> _prototypes = new();

        /// <summary>
        /// Initialise le registre avec les 3 modèles de base
        /// Cette opération est faite UNE SEULE FOIS au démarrage
        /// </summary>
        public void Initialiser()
        {
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("   INITIALISATION DU REGISTRE DES PROTOTYPES");
            Console.WriteLine("   (Cette opération coûteuse n'est faite qu'une fois)");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            // Création des modèles (COÛTEUX - fait une seule fois)
            _prototypes["habitation"] = new ContratHabitation();
            Console.WriteLine();

            _prototypes["automobile"] = new ContratAutomobile();
            Console.WriteLine();

            _prototypes["vie"] = new ContratVie();

            Console.WriteLine("\n═══════════════════════════════════════════════════════");
            Console.WriteLine("   ✅ Registre initialisé avec 3 prototypes");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");
        }

        /// <summary>
        /// Obtient un CLONE du prototype demandé (RAPIDE)
        /// </summary>
        public IContratPrototype ObtenirContrat(string type)
        {
            if (!_prototypes.ContainsKey(type.ToLower()))
            {
                throw new ArgumentException($"Type de contrat inconnu: {type}");
            }

            // CLONAGE au lieu de création !
            return _prototypes[type.ToLower()].Cloner();
        }

        /// <summary>
        /// Ajoute un nouveau prototype personnalisé au registre
        /// Utile pour des variantes fréquemment utilisées
        /// </summary>
        public void AjouterPrototype(string cle, IContratPrototype prototype)
        {
            _prototypes[cle.ToLower()] = prototype;
            Console.WriteLine($"   📌 Nouveau prototype ajouté: {cle}");
        }
    }
}
