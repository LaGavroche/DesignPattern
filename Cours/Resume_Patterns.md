# Resume des Design Patterns

## Vue d'ensemble

```
┌────────────────────────────────────────────────────────────────────────────┐
│                         DESIGN PATTERNS                                     │
├────────────────────────────────────────────────────────────────────────────┤
│                                                                            │
│  CREATIONNELS (Comment creer des objets)                                   │
│  ├── Singleton      → Un seul exemplaire (President)                       │
│  ├── Factory Method → Distributeur automatique                             │
│  ├── Abstract Factory → IKEA vs Maisons du Monde (familles)               │
│  ├── Builder        → Menu burger personnalise (etape par etape)          │
│  └── Prototype      → Photocopie (clonage)                                │
│                                                                            │
│  STRUCTURELS (Comment organiser des objets)                                │
│  ├── Adapter        → Prise electrique UK/FR                              │
│  ├── Decorator      → Options de voiture (couches)                        │
│  ├── Facade         → Telecommande universelle (simplifier)               │
│  ├── Composite      → Dossiers/Fichiers (arborescence)                    │
│  ├── Proxy          → Carte bancaire (intermediaire)                      │
│  └── Bridge         → Telecommande + TV (separer abstraction/impl)        │
│                                                                            │
│  COMPORTEMENTAUX (Comment communiquent les objets)                         │
│  ├── Observer       → Abonnement YouTube (notifications)                  │
│  ├── Strategy       → GPS multi-itineraires (algorithmes)                 │
│  ├── Command        → Boutons telecommande (encapsuler actions + undo)    │
│  ├── State          → Feu de circulation (comportement selon etat)        │
│  ├── Template Method → Recette de cuisine (squelette fixe)                │
│  └── Iterator       → Playlist (parcourir sans exposer)                   │
│                                                                            │
└────────────────────────────────────────────────────────────────────────────┘
```

---

## Patterns implementes dans ce projet

### TP1 - Abstract Factory

**Probleme** : Creer des familles d'objets coherents
**Solution** : Une fabrique par famille

```csharp
IDocumentBancaireFactory factory = new ParticulierDocumentFactory(...);
var rib = factory.CreerRIB();           // RIBParticulier
var attestation = factory.CreerAttestation();  // AttestationParticulier
// Garantie : les deux documents sont coherents (meme famille)
```

---

### TP2 - Prototype

**Probleme** : Creation d'objets couteuse
**Solution** : Cloner un modele pre-charge

```csharp
// Initialisation (1 fois, couteux)
var registre = new RegistreContrats();
registre.Initialiser();  // Charge les clauses standard

// Utilisation (rapide, clone)
var contrat = registre.ObtenirContrat("habitation");  // Clone !
contrat.Personnaliser("Jean Dupont", ...);
```

---

### TP3 - Bridge

**Probleme** : Explosion combinatoire (n types x m plateformes)
**Solution** : Separer abstraction et implementation

```csharp
// L'abstraction (type de notif) est separee de l'implementation (plateforme)
IPlateformeEnvoi plateforme = new EmailPlateforme();
Notification notif = new NotificationCommande(plateforme);
notif.Envoyer("Message", "destinataire");

// Changer de plateforme dynamiquement
notif.ChangerPlateforme(new SMSPlateforme());
notif.Envoyer("Message", "destinataire");  // Meme code, autre plateforme
```

---

## Quand utiliser quel pattern ?

| Besoin | Pattern |
|--------|---------|
| Une seule instance globale | Singleton |
| Creer sans connaitre la classe | Factory Method |
| Familles d'objets coherents | **Abstract Factory** |
| Construction etape par etape | Builder |
| Copier un objet existant | **Prototype** |
| Interfaces incompatibles | Adapter |
| Ajouter des fonctionnalites | Decorator |
| Simplifier un systeme complexe | Facade |
| Structures arborescentes | Composite |
| Controler l'acces | Proxy |
| Separer abstraction/implementation | **Bridge** |
| Notifier plusieurs objets | Observer |
| Changer d'algorithme | Strategy |
| Encapsuler des actions | Command |
| Comportement selon l'etat | State |
