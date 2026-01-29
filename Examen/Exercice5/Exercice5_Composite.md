## Exercice 5 - Analyse UML : Pattern Composite

### 5.1 - Pattern identifié : Composite

Le diagramme montre une structure arborescente où Leaf et Composite implémentent
la même interface IComponent, et Composite contient une collection d'enfants IComponent.
C'est la signature du pattern Composite.

---

### 5.2 - Rôle de chaque participant

- **IComponent** : Interface commune qui définit le contrat (Operation(), Add(), Remove(), GetChild()).
  Elle permet de traiter uniformément les objets simples et les compositions.

- **Leaf** (Feuille) : Élément terminal de l'arborescence, sans enfants.
  Elle implémente Operation() avec son comportement propre. C'est l'unité de base.

- **Composite** : Élément qui contient des enfants (List<IComponent>).
  Son Operation() délègue l'appel à tous ses enfants.
  Il gère l'ajout/suppression d'enfants via Add() et Remove().

---

### 5.3 - Exemple concret

**Domaine : Système de fichiers**

- Leaf = un Fichier (pas d'enfants)
- Composite = un Dossier (contient des fichiers et d'autres dossiers)
- Operation() = calculer la taille totale

Un dossier calcule sa taille en additionnant récursivement la taille de tous ses
fichiers et sous-dossiers. Le client traite fichiers et dossiers de manière uniforme.

---

### 5.4 - Deux approches pour les méthodes inutiles dans Leaf

**Approche 1 : Interface unifiée (transparence)** — Approche actuelle du diagramme

Leaf implémente Add(), Remove(), GetChild() en levant une exception
(throw new NotSupportedException()).

- Avantage : Le client traite Leaf et Composite de façon totalement uniforme
  via IComponent, sans avoir besoin de connaître le type réel.
- Inconvénient : Erreurs détectées uniquement à l'exécution.
  Viole le principe ISP (Interface Segregation Principle).

**Approche 2 : Interface séparée (sécurité)**

Séparer IComponent en deux : une interface de base avec seulement Operation(),
et déplacer Add(), Remove(), GetChild() uniquement dans Composite.

- Avantage : Sécurité au compile-time, impossible d'appeler Add() sur une feuille.
  Respect de l'ISP.
- Inconvénient : Le client doit vérifier le type (if component is Composite)
  avant d'ajouter des enfants, ce qui perd la transparence du pattern.

C'est le compromis classique du Composite : transparence vs sécurité.
