using TP3_Bridge.Implementations;

namespace TP3_Bridge.Abstractions
{
    /// <summary>
    /// REFINED ABSTRACTION : Notification promotionnelle
    /// Démontre la facilité d'ajout d'un nouveau type de notification
    /// </summary>
    public class NotificationPromotion : Notification
    {
        public string CodePromo { get; set; }
        public int PourcentageReduction { get; set; }
        public DateTime DateExpiration { get; set; }

        public NotificationPromotion(IPlateformeEnvoi plateforme) : base(plateforme)
        {
            CodePromo = "";
            PourcentageReduction = 0;
            DateExpiration = DateTime.Now.AddDays(7);
        }

        public override string TypeNotification => "Promotion";

        public override void Envoyer(string message, string destinataire)
        {
            string titre = $"🎁 Offre spéciale : -{PourcentageReduction}%";
            string contenu = $"{message} | Code: {CodePromo} | Expire le {DateExpiration:dd/MM}";

            _plateforme.Envoyer(titre, contenu, destinataire);
        }

        /// <summary>
        /// Notification : nouvelle promotion
        /// </summary>
        public void EnvoyerNouvellePromo(string destinataire)
        {
            Envoyer($"Profitez de -{PourcentageReduction}% sur tout le site !", destinataire);
        }

        /// <summary>
        /// Notification : rappel expiration
        /// </summary>
        public void EnvoyerRappelExpiration(string destinataire)
        {
            var joursRestants = (DateExpiration - DateTime.Now).Days;
            EnvoyerUrgent($"Plus que {joursRestants} jour(s) pour utiliser votre code -{PourcentageReduction}% !", destinataire);
        }
    }
}
