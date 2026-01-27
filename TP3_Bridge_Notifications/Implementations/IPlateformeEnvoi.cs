namespace TP3_Bridge.Implementations
{
    /// <summary>
    /// IMPLEMENTOR (Interface d'implémentation)
    /// Définit l'interface pour toutes les plateformes d'envoi.
    /// C'est le "comment envoyer" - indépendant du type de notification.
    /// </summary>
    public interface IPlateformeEnvoi
    {
        /// <summary>
        /// Envoie un message via cette plateforme
        /// </summary>
        /// <param name="titre">Titre/Sujet du message</param>
        /// <param name="contenu">Contenu du message</param>
        /// <param name="destinataire">Destinataire (email, téléphone, userId...)</param>
        void Envoyer(string titre, string contenu, string destinataire);

        /// <summary>
        /// Nom de la plateforme (pour affichage)
        /// </summary>
        string NomPlateforme { get; }
    }
}
