# TP1 - Abstract Factory : Documents Bancaires

## Enonce

Une banque propose differents types de documents officiels selon le type de client :
- **Particuliers** : RIB simplifie + Attestation standard
- **Professionnels** : RIB detaille avec SIRET + Attestation avec mentions legales

## Pattern utilise : Abstract Factory

### Pourquoi ce pattern ?

- Creer des **familles d'objets coherents** (RIB + Attestation du meme type)
- Garantir qu'un client particulier ne recoit pas un document professionnel
- Faciliter l'ajout de nouveaux types de clients

### Diagramme

```
IDocumentBancaireFactory (Fabrique Abstraite)
├── CreerRIB() : IReleveIdentiteBancaire
└── CreerAttestation() : IAttestationCompte
        │
        ├── ParticulierDocumentFactory
        │   ├── → RIBParticulier
        │   └── → AttestationParticulier
        │
        └── ProfessionnelDocumentFactory
            ├── → RIBProfessionnel
            └── → AttestationProfessionnel
```

## Structure des fichiers

```
TP1_AbstractFactory_Banque/
├── Fabriques/
│   ├── IDocumentBancaireFactory.cs    # Fabrique abstraite
│   ├── ParticulierDocumentFactory.cs  # Fabrique concrete
│   └── ProfessionnelDocumentFactory.cs
├── Produits/
│   ├── IReleveIdentiteBancaire.cs     # Produit abstrait
│   ├── IAttestationCompte.cs          # Produit abstrait
│   ├── Particulier/
│   │   ├── RIBParticulier.cs
│   │   └── AttestationParticulier.cs
│   └── Professionnel/
│       ├── RIBProfessionnel.cs
│       └── AttestationProfessionnel.cs
├── ServiceDocuments.cs                 # Client
├── Program.cs                          # Demo
└── DiagrammeUML.md                     # Diagramme complet
```

## Executer

```bash
dotnet run
```
