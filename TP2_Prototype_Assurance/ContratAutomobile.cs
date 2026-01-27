namespace Assurance
{
    /// <summary>
    /// Prototype concret : Contrat Assurance Automobile
    /// </summary>
    public class ContratAutomobile : ContratAssurance
    {
        public string Immatriculation { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public decimal Franchise { get; set; }
        public bool TousRisques { get; set; }

        /// <summary>
        /// Constructeur pour créer le MODÈLE initial (coûteux)
        /// </summary>
        public ContratAutomobile()
        {
            TypeContrat = "Automobile";
            Console.WriteLine("🚗 Création du modèle Contrat Automobile...");
            ChargerClausesStandard();  // Opération COÛTEUSE

            // Valeurs par défaut
            Franchise = 300m;
            TousRisques = false;
        }

        /// <summary>
        /// Constructeur privé pour le clonage (rapide)
        /// </summary>
        private ContratAutomobile(bool estClone)
        {
            TypeContrat = "Automobile";
        }

        public override IContratPrototype Cloner()
        {
            Console.WriteLine("   📋 Clonage du contrat Automobile (rapide)...");

            var clone = new ContratAutomobile(estClone: true);
            CopierVers(clone);

            clone.Franchise = this.Franchise;
            clone.TousRisques = this.TousRisques;
            clone.Immatriculation = "";
            clone.Marque = "";
            clone.Modele = "";

            return clone;
        }

        public void SetVehicule(string immat, string marque, string modele)
        {
            Immatriculation = immat;
            Marque = marque;
            Modele = modele;
        }

        public void ActiverTousRisques()
        {
            TousRisques = true;
            // La formule tous risques augmente la prime
        }

        public override void Afficher()
        {
            base.Afficher();
            Console.WriteLine($@"   🚗 Détails Automobile:
      Véhicule  : {Marque} {Modele}
      Immat     : {Immatriculation}
      Franchise : {Franchise}€
      Formule   : {(TousRisques ? "✅ Tous Risques" : "Tiers")}
");
        }
    }
}
