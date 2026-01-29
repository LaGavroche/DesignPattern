## Diagramme UML - Singleton (ConfigurationHospitaliere)

```
┌──────────────────────────────────────────────┐
│        ConfigurationHospitaliere             │
├──────────────────────────────────────────────┤
│ - instance : ConfigurationHospitaliere       │
│ - connectionString : string                  │
│ - langue : string                            │
│ - fuseauHoraire : string                     │
├──────────────────────────────────────────────┤
│ - ConfigurationHospitaliere()                │
│ + GetInstance() : ConfigurationHospitaliere   │
│ + GetConnectionString() : string             │
│ + SetConnectionString(value: string)         │
│ + GetLangue() : string                       │
│ + SetLangue(value: string)                   │
│ + GetFuseauHoraire() : string                │
│ + SetFuseauHoraire(value: string)            │
└──────────────────────────────────────────────┘

Légende :
  - : privé
  + : public
  instance et GetInstance() sont statiques (soulignés en UML)
```
