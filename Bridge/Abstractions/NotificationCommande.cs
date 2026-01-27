using TP3_Bridge.Implementations;

namespace TP3_Bridge.Abstractions
{
    /// <summary>
    /// REFINED ABSTRACTION : Notification de commande
    /// Spécialise le comportement pour les notifications liées aux commandes
    /// </summary>
    public class NotificationCommande : Notification
    {
        public string NumeroCommande { get; set; }

        public NotificationCommande(IPlateformeEnvoi plateforme) : base(plateforme)
        {
            NumeroCommande = "";
        }

        public override string TypeNotification => "Commande";

        public override void Envoyer(string message, string destinataire)
        {
            // Formatage spécifique aux commandes
            string titre = $"[Commande #{NumeroCommande}]";
            string contenu = message;

            // Délégation à l'implémentation (la plateforme)
            // C'est ICI que le pont est traversé !
            _plateforme.Envoyer(titre, contenu, destinataire);
        }

        /// <summary>
        /// Méthode spécifique aux commandes : confirmation
        /// </summary>
        public void EnvoyerConfirmation(string destinataire)
        {
            Envoyer($"Votre commande #{NumeroCommande} a été confirmée. Merci !", destinataire);
        }

        /// <summary>
        /// Méthode spécifique aux commandes : annulation
        /// </summary>
        public void EnvoyerAnnulation(string destinataire, string raison)
        {
            EnvoyerUrgent($"Commande #{NumeroCommande} annulée. Raison: {raison}", destinataire);
        }
    }
}
