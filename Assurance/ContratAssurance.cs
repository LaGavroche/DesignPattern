namespace Assurance
{
    /// <summary>
    /// Classe de base abstraite pour tous les contrats
    /// Contient les éléments communs et la logique de clonage
    /// </summary>
    public abstract class ContratAssurance : IContratPrototype
    {
        // Clauses standard - COÛTEUSES à charger (simulé)
        protected List<string> ClausesStandard { get; set; } = new();

        // Informations personnalisées
        public string NomClient { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public decimal MontantPrime { get; set; }

        // Annexes spécifiques
        protected List<string> Annexes { get; set; } = new();

        public string TypeContrat { get; protected set; }

        /// <summary>
        /// Charge les clauses standard - opération COÛTEUSE
        /// Simulée par un délai et beaucoup de données
        /// </summary>
        protected void ChargerClausesStandard()
        {
            Console.WriteLine($"   ⏳ Chargement des clauses standard {TypeContrat}...");
            Thread.Sleep(500);  // Simule un chargement long (BDD, fichiers, etc.)

            // Clauses volumineuses
            for (int i = 1; i <= 50; i++)
            {
                ClausesStandard.Add($"Clause {TypeContrat} n°{i}: Lorem ipsum dolor sit amet...");
            }
            Console.WriteLine($"   ✅ {ClausesStandard.Count} clauses chargées");
        }

        /// <summary>
        /// CLONAGE - Coeur du pattern Prototype
        /// Copie profonde de l'objet sans recharger les clauses
        /// </summary>
        public abstract IContratPrototype Cloner();

        /// <summary>
        /// Clone les données de base (utilisé par les sous-classes)
        /// </summary>
        protected void CopierVers(ContratAssurance cible)
        {
            // Copie profonde des clauses (pas de rechargement !)
            cible.ClausesStandard = new List<string>(this.ClausesStandard);
            cible.Annexes = new List<string>(this.Annexes);
            cible.TypeContrat = this.TypeContrat;

            // Les infos personnalisées seront écrasées après
            cible.NomClient = "";
            cible.MontantPrime = 0;
        }

        public void Personnaliser(string nomClient, DateTime dateDebut, decimal montant)
        {
            NomClient = nomClient;
            DateDebut = dateDebut;
            DateFin = dateDebut.AddYears(1);
            MontantPrime = montant;
        }

        public void AjouterAnnexe(string annexe)
        {
            Annexes.Add(annexe);
        }

        public virtual void Afficher()
        {
            Console.WriteLine($@"
╔══════════════════════════════════════════════════════╗
║  CONTRAT {TypeContrat.ToUpper(),-42} ║
╠══════════════════════════════════════════════════════╣
║  Client      : {NomClient,-37} ║
║  Période     : {DateDebut:dd/MM/yyyy} au {DateFin:dd/MM/yyyy,-14} ║
║  Prime       : {MontantPrime,10:N2}€ / an                    ║
╠══════════════════════════════════════════════════════╣
║  Clauses     : {ClausesStandard.Count} clauses standard incluses       ║
║  Annexes     : {Annexes.Count} annexe(s) spécifique(s)             ║
╚══════════════════════════════════════════════════════╝");
        }
    }
}
