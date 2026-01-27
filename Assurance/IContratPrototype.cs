namespace Assurance
{
    /// <summary>
    /// Interface Prototype
    /// Définit la capacité de clonage pour tous les contrats
    /// </summary>
    public interface IContratPrototype
    {
        // Méthode de clonage - coeur du pattern Prototype
        IContratPrototype Cloner();

        void Personnaliser(string nomClient, DateTime dateDebut, decimal montant);
        void AjouterAnnexe(string annexe);
        void Afficher();
    }
}
