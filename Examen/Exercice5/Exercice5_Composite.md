## Exercice 5 - Analyse UML : Pattern Composite

### 5.1 - Pattern identifié : Composite

C'est la signature du pattern Composite.
On voit bien une structure arborescente donc Coposite on a meme une collection d'enfants Icomponent c'est la signature de ce pattern

---

### 5.2 - Rôle de chaque participant

- IComponent : Interface commune qui définit le contrat (Operation(), Add(), Remove(), GetChild()).
  Elle permet de traiter uniformément les objets simples et les compositions.

- Leaf: Élément terminal de l'arborescence, sans enfants.
  Elle implémente Operation() avec son comportement propre. C'est l'unité de base.

- Composite : Élément qui contient des enfants (List<IComponent>).
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


