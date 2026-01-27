using TP3_Bridge.Implementations;

namespace TP3_Bridge.Abstractions
{
    /// <summary>
    /// REFINED ABSTRACTION : Notification de livraison
    /// Spécialise le comportement pour les notifications de suivi de colis
    /// </summary>
    public class NotificationLivraison : Notification
    {
        public string NumeroSuivi { get; set; }
        public string Transporteur { get; set; }

        public NotificationLivraison(IPlateformeEnvoi plateforme) : base(plateforme)
        {
            NumeroSuivi = "";
            Transporteur = "Standard";
        }

        public override string TypeNotification => "Livraison";

        public override void Envoyer(string message, string destinataire)
        {
            string titre = $"📦 Livraison {Transporteur}";
            string contenu = $"[{NumeroSuivi}] {message}";

            _plateforme.Envoyer(titre, contenu, destinataire);
        }

        /// <summary>
        /// Notification : colis expédié
        /// </summary>
        public void EnvoyerExpedition(string destinataire)
        {
            Envoyer("Votre colis a été expédié et est en route !", destinataire);
        }

        /// <summary>
        /// Notification : colis en cours de livraison
        /// </summary>
        public void EnvoyerEnCoursLivraison(string destinataire, string creneauHoraire)
        {
            EnvoyerUrgent($"Livraison prévue aujourd'hui entre {creneauHoraire}", destinataire);
        }

        /// <summary>
        /// Notification : colis livré
        /// </summary>
        public void EnvoyerLivre(string destinataire, string lieuDepot)
        {
            Envoyer($"Votre colis a été livré. Déposé : {lieuDepot}", destinataire);
        }
    }
}
