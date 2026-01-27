using TP3_Bridge.Implementations;

namespace TP3_Bridge.Abstractions
{
    /// <summary>
    /// REFINED ABSTRACTION : Notification de support client
    /// Spécialise le comportement pour les notifications du service client
    /// </summary>
    public class NotificationSupport : Notification
    {
        public string NumeroTicket { get; set; }
        public string NomAgent { get; set; }

        public NotificationSupport(IPlateformeEnvoi plateforme) : base(plateforme)
        {
            NumeroTicket = "";
            NomAgent = "Support Client";
        }

        public override string TypeNotification => "Support";

        public override void Envoyer(string message, string destinataire)
        {
            string titre = $"🎧 Support - Ticket #{NumeroTicket}";
            string contenu = $"{message} - {NomAgent}";

            _plateforme.Envoyer(titre, contenu, destinataire);
        }

        /// <summary>
        /// Notification : ticket créé
        /// </summary>
        public void EnvoyerCreationTicket(string destinataire)
        {
            Envoyer($"Votre demande a été enregistrée. Nous vous répondrons sous 24h.", destinataire);
        }

        /// <summary>
        /// Notification : réponse de l'agent
        /// </summary>
        public void EnvoyerReponseAgent(string destinataire, string extrait)
        {
            Envoyer($"Nouvelle réponse : \"{extrait}...\"", destinataire);
        }

        /// <summary>
        /// Notification : ticket résolu
        /// </summary>
        public void EnvoyerResolution(string destinataire)
        {
            Envoyer("Votre ticket a été résolu. N'hésitez pas à nous recontacter !", destinataire);
        }
    }
}
