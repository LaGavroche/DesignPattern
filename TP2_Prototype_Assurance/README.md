# TP2 - Prototype : Contrats d'Assurance

## Enonce

Une compagnie d'assurance doit generer de nombreux contrats. Chaque contrat contient :
- Des clauses standard (identiques pour un meme type)
- Des informations personnalisees
- Des annexes specifiques

La creation d'un contrat est **couteuse** car elle necessite de charger toutes les clauses.

## Pattern utilise : Prototype

### Pourquoi ce pattern ?

- **Performance** : Cloner au lieu de recreer (evite le rechargement des clauses)
- **Variations** : Creer facilement des variantes d'un meme contrat
- **Flexibilite** : Ajouter des prototypes personnalises au registre

### Diagramme

```
RegistreContrats (Registry)
│
├── "habitation" → ContratHabitation (prototype)
├── "automobile" → ContratAutomobile (prototype)
└── "vie"        → ContratVie (prototype)

Client:
  contrat = registre.ObtenirContrat("habitation")  // Clone, pas new !
  contrat.Personnaliser(...)
```

### Comparaison de performance

| Operation | Sans Prototype | Avec Prototype |
|-----------|---------------|----------------|
| Init 3 modeles | N/A | ~1500ms |
| Creer 4 contrats | ~2000ms | ~20ms |

## Structure des fichiers

```
TP2_Prototype_Assurance/
├── IContratPrototype.cs        # Interface Prototype
├── ContratAssurance.cs         # Classe abstraite
├── ContratHabitation.cs        # Prototype concret
├── ContratAutomobile.cs        # Prototype concret
├── ContratVie.cs               # Prototype concret
├── RegistreContrats.cs         # Registry des prototypes
├── Program.cs                  # Demo
└── DiagrammeUML.md             # Diagramme complet
```

## Executer

```bash
dotnet run
```
