using TP3_Bridge.Implementations;

namespace TP3_Bridge.Abstractions
{
    /// <summary>
    /// ABSTRACTION (Classe abstraite)
    /// Définit l'interface de haut niveau pour les notifications.
    /// C'est le "quoi envoyer" - contient une référence vers l'implémentation.
    ///
    /// Le PONT (Bridge) est la référence _plateforme qui lie l'abstraction
    /// à son implémentation.
    /// </summary>
    public abstract class Notification
    {
        // ═══════════════════════════════════════════════════════════════
        //  LE PONT : référence vers l'implémentation
        // ═══════════════════════════════════════════════════════════════
        protected IPlateformeEnvoi _plateforme;

        /// <summary>
        /// Constructeur : injection de l'implémentation (le pont)
        /// </summary>
        protected Notification(IPlateformeEnvoi plateforme)
        {
            _plateforme = plateforme;
        }

        /// <summary>
        /// Permet de changer de plateforme dynamiquement
        /// </summary>
        public void ChangerPlateforme(IPlateformeEnvoi nouvellePlateforme)
        {
            _plateforme = nouvellePlateforme;
        }

        /// <summary>
        /// Type de notification (pour affichage)
        /// </summary>
        public abstract string TypeNotification { get; }

        /// <summary>
        /// Méthode principale : envoie la notification
        /// Chaque type de notification implémente sa propre logique
        /// mais délègue l'envoi réel à la plateforme (implémentation)
        /// </summary>
        public abstract void Envoyer(string message, string destinataire);

        /// <summary>
        /// Envoie avec urgence (comportement enrichi dans l'abstraction)
        /// </summary>
        public virtual void EnvoyerUrgent(string message, string destinataire)
        {
            string messageUrgent = $"🚨 URGENT: {message}";
            Envoyer(messageUrgent, destinataire);
        }
    }
}
