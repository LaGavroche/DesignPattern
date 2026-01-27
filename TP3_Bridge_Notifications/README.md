# TP3 - Bridge : Notifications Multi-plateforme

## Enonce

Un systeme de e-commerce doit gerer :
- Differents **types** de notifications (Commande, Livraison, Support)
- Differentes **plateformes** d'envoi (Email, SMS, Push)

Le code existant avait 9 methodes dupliquees (3 x 3). Avec 4 types et 4 plateformes, on aurait 16 methodes !

## Pattern utilise : Bridge

### Pourquoi ce pattern ?

- **Eviter l'explosion combinatoire** : n + m classes au lieu de n x m
- **Separation des responsabilites** : "quoi envoyer" vs "comment envoyer"
- **Evolution independante** : ajouter un type OU une plateforme sans toucher l'autre

### Diagramme

```
ABSTRACTION (Quoi)                      IMPLEMENTATION (Comment)
──────────────────                      ────────────────────────

Notification (abstract)                 IPlateformeEnvoi
├── NotificationCommande                ├── EmailPlateforme
├── NotificationLivraison    ═══════    ├── SMSPlateforme
├── NotificationSupport        PONT     ├── PushPlateforme
└── NotificationPromotion               └── DiscordPlateforme
```

### Avantages

| Avant (sans Bridge) | Apres (avec Bridge) |
|---------------------|---------------------|
| 3 types x 3 plateformes = 9 methodes | 4 types + 4 plateformes = 8 classes |
| Ajouter Discord = modifier 3 classes | Ajouter Discord = 1 nouvelle classe |
| Code duplique | Zero duplication |

## Structure des fichiers

```
TP3_Bridge_Notifications/
├── Abstractions/                       # QUOI envoyer
│   ├── Notification.cs                 # Abstraction
│   ├── NotificationCommande.cs         # Refined Abstraction
│   ├── NotificationLivraison.cs
│   ├── NotificationSupport.cs
│   └── NotificationPromotion.cs        # Nouveau type (demo)
├── Implementations/                    # COMMENT envoyer
│   ├── IPlateformeEnvoi.cs            # Implementor
│   ├── EmailPlateforme.cs             # Concrete Implementor
│   ├── SMSPlateforme.cs
│   ├── PushPlateforme.cs
│   └── DiscordPlateforme.cs           # Nouvelle plateforme (demo)
├── Program.cs                          # Demo
└── DiagrammeUML.md                     # Diagramme complet
```

## Executer

```bash
dotnet run
```
