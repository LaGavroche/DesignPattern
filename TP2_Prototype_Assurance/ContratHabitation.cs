namespace Assurance
{
    /// <summary>
    /// Prototype concret : Contrat Assurance Habitation
    /// </summary>
    public class ContratHabitation : ContratAssurance
    {
        public string AdresseBien { get; set; }
        public decimal Franchise { get; set; }
        public bool OptionVolProtection { get; set; }

        /// <summary>
        /// Constructeur pour créer le MODÈLE initial (coûteux)
        /// </summary>
        public ContratHabitation()
        {
            TypeContrat = "Habitation";
            Console.WriteLine("🏠 Création du modèle Contrat Habitation...");
            ChargerClausesStandard();  // Opération COÛTEUSE

            // Valeurs par défaut du modèle
            Franchise = 150m;
            OptionVolProtection = false;
        }

        /// <summary>
        /// Constructeur privé pour le clonage (rapide)
        /// </summary>
        private ContratHabitation(bool estClone)
        {
            TypeContrat = "Habitation";
            // Pas de chargement des clauses !
        }

        /// <summary>
        /// CLONAGE - Copie rapide sans rechargement
        /// </summary>
        public override IContratPrototype Cloner()
        {
            Console.WriteLine("   📋 Clonage du contrat Habitation (rapide)...");

            var clone = new ContratHabitation(estClone: true);

            // Copie des données de base
            CopierVers(clone);

            // Copie des données spécifiques Habitation
            clone.Franchise = this.Franchise;
            clone.OptionVolProtection = this.OptionVolProtection;
            clone.AdresseBien = "";  // À personnaliser

            return clone;
        }

        public void SetAdresse(string adresse) => AdresseBien = adresse;
        public void SetFranchise(decimal franchise) => Franchise = franchise;
        public void ActiverOptionVol() => OptionVolProtection = true;

        public override void Afficher()
        {
            base.Afficher();
            Console.WriteLine($@"   🏠 Détails Habitation:
      Adresse   : {AdresseBien}
      Franchise : {Franchise}€
      Option Vol: {(OptionVolProtection ? "✅ Oui" : "❌ Non")}
");
        }
    }
}
