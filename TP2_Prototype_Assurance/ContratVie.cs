namespace Assurance
{
    /// <summary>
    /// Prototype concret : Contrat Assurance Vie
    /// </summary>
    public class ContratVie : ContratAssurance
    {
        public decimal CapitalGaranti { get; set; }
        public string Beneficiaire { get; set; }
        public int DureeAnnees { get; set; }
        public bool OptionDeces { get; set; }
        public bool OptionInvalidite { get; set; }

        /// <summary>
        /// Constructeur pour créer le MODÈLE initial (coûteux)
        /// </summary>
        public ContratVie()
        {
            TypeContrat = "Vie";
            Console.WriteLine("💚 Création du modèle Contrat Vie...");
            ChargerClausesStandard();  // Opération COÛTEUSE

            // Valeurs par défaut
            DureeAnnees = 20;
            OptionDeces = true;
            OptionInvalidite = false;
        }

        /// <summary>
        /// Constructeur privé pour le clonage (rapide)
        /// </summary>
        private ContratVie(bool estClone)
        {
            TypeContrat = "Vie";
        }

        public override IContratPrototype Cloner()
        {
            Console.WriteLine("   📋 Clonage du contrat Vie (rapide)...");

            var clone = new ContratVie(estClone: true);
            CopierVers(clone);

            clone.DureeAnnees = this.DureeAnnees;
            clone.OptionDeces = this.OptionDeces;
            clone.OptionInvalidite = this.OptionInvalidite;
            clone.CapitalGaranti = 0;
            clone.Beneficiaire = "";

            return clone;
        }

        public void SetCapital(decimal capital) => CapitalGaranti = capital;
        public void SetBeneficiaire(string beneficiaire) => Beneficiaire = beneficiaire;
        public void ActiverOptionInvalidite() => OptionInvalidite = true;

        public override void Afficher()
        {
            base.Afficher();
            Console.WriteLine($@"   💚 Détails Vie:
      Capital       : {CapitalGaranti:N0}€
      Bénéficiaire  : {Beneficiaire}
      Durée         : {DureeAnnees} ans
      Option Décès  : {(OptionDeces ? "✅" : "❌")}
      Option Inval. : {(OptionInvalidite ? "✅" : "❌")}
");
        }
    }
}
