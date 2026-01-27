# Design Patterns en C#

Implémentation des principaux Design Patterns en C# avec des cas d'usage concrets.

## Structure du projet

```
DesignPatterns/
│
├── Cours/                              # Documentation et cours
│   └── Cours_Design_Patterns.md        # Guide complet des patterns
│
├── TP1_AbstractFactory_Banque/         # Pattern Abstract Factory
│   └── Système de documents bancaires
│
├── TP2_Prototype_Assurance/            # Pattern Prototype
│   └── Génération de contrats d'assurance
│
├── TP3_Bridge_Notifications/           # Pattern Bridge
│   └── Système de notifications multi-plateforme
│
└── _Archives/                          # Anciens fichiers de test
```

---

## TP1 - Abstract Factory : Documents Bancaires

**Pattern** : Abstract Factory (Fabrique Abstraite)

**Problème** : Créer des familles de documents cohérents selon le type de client (Particulier / Professionnel)

**Solution** : Une fabrique abstraite qui produit des RIB + Attestations adaptés au type de client

```
IDocumentBancaireFactory
├── ParticulierDocumentFactory  → RIBParticulier + AttestationParticulier
└── ProfessionnelDocumentFactory → RIBProfessionnel + AttestationProfessionnel
```

**Exécuter** :
```bash
cd TP1_AbstractFactory_Banque
dotnet run
```

---

## TP2 - Prototype : Contrats d'Assurance

**Pattern** : Prototype

**Problème** : Créer de nombreux contrats sans recharger les clauses standard à chaque fois (coûteux)

**Solution** : Cloner des contrats modèles pré-chargés

```
RegistreContrats (Registry)
├── ContratHabitation (prototype)  → clone rapide
├── ContratAutomobile (prototype)  → clone rapide
└── ContratVie (prototype)         → clone rapide
```

**Exécuter** :
```bash
cd TP2_Prototype_Assurance
dotnet run
```

---

## TP3 - Bridge : Notifications Multi-plateforme

**Pattern** : Bridge (Pont)

**Problème** : Éviter l'explosion combinatoire (3 types x 3 plateformes = 9 classes)

**Solution** : Séparer l'abstraction (type de notif) de l'implémentation (plateforme d'envoi)

```
ABSTRACTION (Quoi)              PONT              IMPLEMENTATION (Comment)
─────────────────               ────              ────────────────────────
NotificationCommande    ─────────┼──────────────  EmailPlateforme
NotificationLivraison            │                SMSPlateforme
NotificationSupport              │                PushPlateforme
NotificationPromotion            │                DiscordPlateforme
```

**Exécuter** :
```bash
cd TP3_Bridge_Notifications
dotnet run
```

---

## Resume des Patterns

| Pattern | Categorie | Probleme resolu | Analogie |
|---------|-----------|-----------------|----------|
| **Abstract Factory** | Creationnel | Creer des familles d'objets coherents | IKEA vs Maisons du Monde |
| **Prototype** | Creationnel | Cloner au lieu de creer (performance) | Photocopieuse |
| **Bridge** | Structurel | Separer abstraction et implementation | Telecommande universelle |

---

## Prerequis

- .NET 8.0 SDK
- Visual Studio 2022 / VS Code / Rider

## Auteur

Rayan Boiteau - 2024
