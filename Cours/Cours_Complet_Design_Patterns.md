# Cours Complet : Les Design Patterns

> **Objectif** : Comprendre les 23 patterns du Gang of Four (GoF) avec des analogies simples et des exemples concrets.

---

## Table des matières

1. [Introduction](#introduction)
2. [Patterns Créationnels](#1-patterns-créationnels) (Comment créer des objets)
3. [Patterns Structurels](#2-patterns-structurels) (Comment organiser les objets)
4. [Patterns Comportementaux](#3-patterns-comportementaux) (Comment les objets communiquent)
5. [Cheat Sheet](#cheat-sheet)

---

## Introduction

### C'est quoi un Design Pattern ?

Un **Design Pattern** (patron de conception) est une **solution réutilisable** à un problème récurrent en programmation. C'est comme une recette de cuisine : tu ne réinventes pas la roue à chaque fois.

### Les 3 catégories

```
┌─────────────────────────────────────────────────────────────────┐
│                      DESIGN PATTERNS                            │
├───────────────────┬───────────────────┬─────────────────────────┤
│   CRÉATIONNELS    │   STRUCTURELS     │    COMPORTEMENTAUX      │
│   (5 patterns)    │   (7 patterns)    │    (11 patterns)        │
├───────────────────┼───────────────────┼─────────────────────────┤
│ Comment CRÉER     │ Comment ORGANISER │ Comment faire           │
│ des objets ?      │ les objets ?      │ COMMUNIQUER les objets? │
└───────────────────┴───────────────────┴─────────────────────────┘
```

---

# 1. Patterns Créationnels

> **But** : Abstraire le processus de création d'objets

---

## 1.1 Singleton

### L'analogie
🏛️ **Le Président de la République** : Il n'y en a qu'UN seul à la fois. Peu importe combien de fois tu demandes "qui est le président ?", c'est toujours la même personne.

### Le problème
Tu veux qu'une classe n'ait **qu'une seule instance** dans toute l'application (ex: connexion BDD, configuration, logger).

### La solution
```
┌─────────────────────────────────────────┐
│              Singleton                  │
├─────────────────────────────────────────┤
│ - instance: Singleton (static)          │
│ - Singleton() (constructeur PRIVÉ)      │
├─────────────────────────────────────────┤
│ + GetInstance(): Singleton (static)     │
└─────────────────────────────────────────┘
```

### Le code
```csharp
public class Configuration
{
    // L'unique instance (static)
    private static Configuration? _instance;
    private static readonly object _lock = new();

    // Constructeur PRIVÉ = personne ne peut faire "new"
    private Configuration() { }

    // Point d'accès global
    public static Configuration Instance
    {
        get
        {
            lock (_lock)  // Thread-safe
            {
                _instance ??= new Configuration();
                return _instance;
            }
        }
    }

    public string Theme { get; set; } = "dark";
}

// Utilisation
var config1 = Configuration.Instance;
var config2 = Configuration.Instance;
// config1 == config2 → TRUE (même objet)
```

### Quand l'utiliser ?
- ✅ Connexion base de données
- ✅ Configuration de l'application
- ✅ Logger
- ❌ Évite si possible (rend les tests difficiles)

---

## 1.2 Factory Method

### L'analogie
🏭 **Un distributeur automatique** : Tu appuies sur "A3", tu reçois un produit. Tu ne sais pas comment il est stocké ou fabriqué, tu reçois juste le résultat.

### Le problème
Tu veux créer des objets **sans spécifier leur classe exacte**. Le code client ne doit pas dépendre des classes concrètes.

### La solution
```
┌─────────────────────────┐
│   Creator (abstract)    │
├─────────────────────────┤
│ + CreateProduct()       │ ←── Factory Method (abstract)
│ + DoSomething()         │     utilise CreateProduct()
└────────────┬────────────┘
             │ hérite
    ┌────────┴────────┐
    ▼                 ▼
┌─────────────┐  ┌─────────────┐
│ CreatorA    │  │ CreatorB    │
├─────────────┤  ├─────────────┤
│+CreateProduct│ │+CreateProduct│
│ → ProductA  │  │ → ProductB  │
└─────────────┘  └─────────────┘
```

### Le code
```csharp
// Produit abstrait
public interface INotification
{
    void Send(string message);
}

// Produits concrets
public class EmailNotification : INotification
{
    public void Send(string message) => Console.WriteLine($"Email: {message}");
}

public class SmsNotification : INotification
{
    public void Send(string message) => Console.WriteLine($"SMS: {message}");
}

// Creator abstrait
public abstract class NotificationCreator
{
    // Factory Method (à implémenter)
    public abstract INotification CreateNotification();

    // Logique métier qui utilise le produit
    public void Notify(string message)
    {
        var notification = CreateNotification();  // Appel de la factory
        notification.Send(message);
    }
}

// Creators concrets
public class EmailCreator : NotificationCreator
{
    public override INotification CreateNotification() => new EmailNotification();
}

public class SmsCreator : NotificationCreator
{
    public override INotification CreateNotification() => new SmsNotification();
}

// Utilisation
NotificationCreator creator = new EmailCreator();
creator.Notify("Hello!");  // Email: Hello!
```

### Quand l'utiliser ?
- ✅ Tu ne connais pas à l'avance le type exact d'objet
- ✅ Tu veux permettre l'extension sans modifier le code existant
- ✅ Frameworks et bibliothèques

---

## 1.3 Abstract Factory

### L'analogie
🛋️ **IKEA vs Maisons du Monde** : Chaque magasin vend des meubles (chaise, table, canapé), mais dans un STYLE différent. Si tu achètes chez IKEA, tous tes meubles sont cohérents entre eux.

### Le problème
Tu veux créer des **familles d'objets liés** qui doivent être cohérents entre eux.

### La solution
```
┌─────────────────────────────────────────────────────────────────┐
│                                                                 │
│  AbstractFactory                      ConcreteFactoryA          │
│  ├─ CreateChair()        ────────►   ├─ CreateChair() → ChairA │
│  ├─ CreateTable()                    ├─ CreateTable() → TableA │
│  └─ CreateSofa()                     └─ CreateSofa()  → SofaA  │
│                                                                 │
│                          ConcreteFactoryB                       │
│             ────────►   ├─ CreateChair() → ChairB              │
│                         ├─ CreateTable() → TableB              │
│                         └─ CreateSofa()  → SofaB               │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

### Le code
```csharp
// Produits abstraits
public interface IButton { void Render(); }
public interface ICheckbox { void Render(); }

// Famille Windows
public class WindowsButton : IButton
{
    public void Render() => Console.WriteLine("[Windows Button]");
}
public class WindowsCheckbox : ICheckbox
{
    public void Render() => Console.WriteLine("[Windows Checkbox]");
}

// Famille MacOS
public class MacButton : IButton
{
    public void Render() => Console.WriteLine("[Mac Button]");
}
public class MacCheckbox : ICheckbox
{
    public void Render() => Console.WriteLine("[Mac Checkbox]");
}

// Abstract Factory
public interface IUIFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}

// Concrete Factories
public class WindowsFactory : IUIFactory
{
    public IButton CreateButton() => new WindowsButton();
    public ICheckbox CreateCheckbox() => new WindowsCheckbox();
}

public class MacFactory : IUIFactory
{
    public IButton CreateButton() => new MacButton();
    public ICheckbox CreateCheckbox() => new MacCheckbox();
}

// Utilisation
IUIFactory factory = new MacFactory();
var button = factory.CreateButton();      // MacButton
var checkbox = factory.CreateCheckbox();  // MacCheckbox
// Garantie : les deux sont du même style !
```

### Factory Method vs Abstract Factory

| Factory Method | Abstract Factory |
|----------------|------------------|
| Crée UN type de produit | Crée une FAMILLE de produits |
| Une méthode | Plusieurs méthodes |
| Héritage | Composition |

---

## 1.4 Builder

### L'analogie
🍔 **Commander un burger personnalisé** : Tu choisis étape par étape : pain, viande, fromage, sauce, salade... À la fin, on te donne ton burger complet.

### Le problème
Tu dois construire un objet complexe **étape par étape**, avec plusieurs configurations possibles.

### La solution
```
┌─────────────────┐      ┌─────────────────┐      ┌─────────────────┐
│    Director     │─────►│     Builder     │─────►│    Product      │
│                 │      │ (interface)     │      │                 │
│ Construct()     │      │ BuildPartA()    │      │ PartA           │
│                 │      │ BuildPartB()    │      │ PartB           │
│                 │      │ GetResult()     │      │ PartC           │
└─────────────────┘      └─────────────────┘      └─────────────────┘
```

### Le code
```csharp
// Le produit complexe
public class Pizza
{
    public string Pate { get; set; } = "";
    public string Sauce { get; set; } = "";
    public List<string> Garnitures { get; set; } = new();

    public override string ToString()
        => $"Pizza: {Pate}, {Sauce}, [{string.Join(", ", Garnitures)}]";
}

// Le Builder
public class PizzaBuilder
{
    private Pizza _pizza = new();

    public PizzaBuilder Reset()
    {
        _pizza = new Pizza();
        return this;
    }

    public PizzaBuilder SetPate(string pate)
    {
        _pizza.Pate = pate;
        return this;  // Retourne this pour le chaînage
    }

    public PizzaBuilder SetSauce(string sauce)
    {
        _pizza.Sauce = sauce;
        return this;
    }

    public PizzaBuilder AddGarniture(string garniture)
    {
        _pizza.Garnitures.Add(garniture);
        return this;
    }

    public Pizza Build() => _pizza;
}

// Utilisation avec chaînage (fluent interface)
var pizza = new PizzaBuilder()
    .SetPate("fine")
    .SetSauce("tomate")
    .AddGarniture("mozzarella")
    .AddGarniture("jambon")
    .AddGarniture("champignons")
    .Build();

Console.WriteLine(pizza);
// Pizza: fine, tomate, [mozzarella, jambon, champignons]
```

### Quand l'utiliser ?
- ✅ Objet avec beaucoup de paramètres optionnels
- ✅ Construction en plusieurs étapes
- ✅ Même processus pour différentes représentations

---

## 1.5 Prototype

### L'analogie
📄 **La photocopieuse** : Au lieu de réécrire un document à chaque fois, tu le photocopies et tu modifies juste ce qui change.

### Le problème
Créer un objet est **coûteux** (chargement BDD, calculs complexes). Tu veux créer des copies d'un objet existant.

### La solution
```
┌─────────────────────────┐
│      Prototype          │
│     (interface)         │
├─────────────────────────┤
│ + Clone(): Prototype    │
└─────────────────────────┘
          ▲
          │ implémente
┌─────────────────────────┐
│   ConcretePrototype     │
├─────────────────────────┤
│ - field1, field2...     │
├─────────────────────────┤
│ + Clone(): Prototype    │ ──► return new CP(this)
└─────────────────────────┘
```

### Le code
```csharp
public class ContratAssurance : ICloneable
{
    public string Type { get; set; } = "";
    public List<string> Clauses { get; set; } = new();
    public decimal PrimeBase { get; set; }

    // Clone profond (deep copy)
    public object Clone()
    {
        return new ContratAssurance
        {
            Type = this.Type,
            Clauses = new List<string>(this.Clauses),  // Copie la liste
            PrimeBase = this.PrimeBase
        };
    }
}

// Registre de prototypes (cache)
public class RegistreContrats
{
    private Dictionary<string, ContratAssurance> _prototypes = new();

    public void Initialiser()
    {
        // Chargement coûteux (1 seule fois)
        var auto = new ContratAssurance
        {
            Type = "Auto",
            Clauses = new() { "Responsabilité civile", "Vol", "Incendie" },
            PrimeBase = 500
        };
        _prototypes["auto"] = auto;
    }

    public ContratAssurance GetContrat(string type)
    {
        return (ContratAssurance)_prototypes[type].Clone();  // Clone rapide
    }
}

// Utilisation
var registre = new RegistreContrats();
registre.Initialiser();  // Coûteux, mais 1 seule fois

var contrat1 = registre.GetContrat("auto");  // Clone rapide
var contrat2 = registre.GetContrat("auto");  // Clone rapide

contrat1.Clauses.Add("Bris de glace");  // Modifie seulement contrat1
```

### Shallow Copy vs Deep Copy

| Shallow Copy | Deep Copy |
|--------------|-----------|
| Copie les références | Copie les valeurs |
| Les objets imbriqués sont partagés | Les objets imbriqués sont dupliqués |
| `MemberwiseClone()` | Copie manuelle ou sérialisation |

---

# 2. Patterns Structurels

> **But** : Organiser les classes et objets pour former des structures plus grandes

---

## 2.1 Adapter

### L'analogie
🔌 **Adaptateur de prise électrique** : Ta prise française ne rentre pas dans une prise anglaise. L'adaptateur fait le pont entre les deux.

### Le problème
Tu as deux interfaces **incompatibles** et tu veux les faire fonctionner ensemble.

### La solution
```
┌─────────────┐     ┌─────────────┐     ┌─────────────┐
│   Client    │────►│   Target    │     │   Adaptee   │
│             │     │ (interface) │     │ (externe)   │
└─────────────┘     └──────▲──────┘     └──────▲──────┘
                          │                    │
                    ┌─────┴──────────────────┐ │
                    │       Adapter          │ │
                    │                        │─┘
                    │ - adaptee: Adaptee     │ composition
                    │ + MethodeTarget()      │───► appelle adaptee.MethodeAdaptee()
                    └────────────────────────┘
```

### Le code
Voir **TP4** pour l'exemple complet avec PaymentPro !

### Quand l'utiliser ?
- ✅ Intégrer une bibliothèque externe avec une interface différente
- ✅ Utiliser du code legacy dans un nouveau système
- ✅ Faire communiquer des systèmes incompatibles

---

## 2.2 Bridge

### L'analogie
📺 **Télécommande + TV** : La télécommande (abstraction) est séparée de la TV (implémentation). Tu peux avoir différentes télécommandes (basique, avancée) et différentes TV (Sony, Samsung) sans explosion combinatoire.

### Le problème
Tu as une **explosion combinatoire** : N types × M implémentations = N×M classes.

### La solution
Séparer l'**abstraction** (ce que tu fais) de l'**implémentation** (comment tu le fais).

```
┌─────────────────┐         ┌─────────────────┐
│   Abstraction   │────────►│ Implementation  │
│                 │         │   (interface)   │
│ - impl          │         └────────▲────────┘
│ + Operation()   │                  │
└────────▲────────┘         ┌────────┴────────┐
         │                  │                 │
┌────────┴────────┐  ┌──────┴──────┐  ┌──────┴──────┐
│ RefinedAbstract │  │   ImplA     │  │   ImplB     │
└─────────────────┘  └─────────────┘  └─────────────┘
```

### Le code
```csharp
// Implementation (comment envoyer)
public interface IMessageSender
{
    void Send(string message, string recipient);
}

public class EmailSender : IMessageSender
{
    public void Send(string message, string recipient)
        => Console.WriteLine($"Email à {recipient}: {message}");
}

public class SmsSender : IMessageSender
{
    public void Send(string message, string recipient)
        => Console.WriteLine($"SMS à {recipient}: {message}");
}

// Abstraction (quel type de message)
public abstract class Notification
{
    protected IMessageSender _sender;

    protected Notification(IMessageSender sender) => _sender = sender;

    public abstract void Notify(string message, string recipient);
}

public class AlertNotification : Notification
{
    public AlertNotification(IMessageSender sender) : base(sender) { }

    public override void Notify(string message, string recipient)
    {
        _sender.Send($"⚠️ ALERTE: {message}", recipient);
    }
}

public class InfoNotification : Notification
{
    public InfoNotification(IMessageSender sender) : base(sender) { }

    public override void Notify(string message, string recipient)
    {
        _sender.Send($"ℹ️ Info: {message}", recipient);
    }
}

// Utilisation : 2 types × 2 canaux = 4 combinaisons, mais seulement 4 classes !
var alertEmail = new AlertNotification(new EmailSender());
var alertSms = new AlertNotification(new SmsSender());
var infoEmail = new InfoNotification(new EmailSender());

alertEmail.Notify("Serveur down!", "admin@company.com");
```

---

## 2.3 Composite

### L'analogie
📁 **Dossiers et fichiers** : Un dossier peut contenir des fichiers ET d'autres dossiers. Tu traites les deux de la même manière (copier, supprimer, déplacer).

### Le problème
Tu as une structure **arborescente** et tu veux traiter les éléments simples et composés de manière uniforme.

### La solution
```
                    ┌─────────────────┐
                    │   Component     │
                    │   (interface)   │
                    │ + Operation()   │
                    └────────▲────────┘
                             │
              ┌──────────────┴──────────────┐
              │                             │
     ┌────────┴────────┐          ┌────────┴────────┐
     │      Leaf       │          │    Composite    │
     │                 │          │                 │
     │ + Operation()   │          │ - children[]    │
     └─────────────────┘          │ + Add(Component)│
                                  │ + Operation()   │ ──► pour chaque enfant
                                  └─────────────────┘       appeler Operation()
```

### Le code
```csharp
// Component
public interface IFileSystemItem
{
    string Name { get; }
    long GetSize();
    void Display(int indent = 0);
}

// Leaf (feuille)
public class File : IFileSystemItem
{
    public string Name { get; }
    public long Size { get; }

    public File(string name, long size)
    {
        Name = name;
        Size = size;
    }

    public long GetSize() => Size;

    public void Display(int indent = 0)
        => Console.WriteLine($"{new string(' ', indent)}📄 {Name} ({Size} Ko)");
}

// Composite
public class Folder : IFileSystemItem
{
    public string Name { get; }
    private List<IFileSystemItem> _children = new();

    public Folder(string name) => Name = name;

    public void Add(IFileSystemItem item) => _children.Add(item);

    public long GetSize() => _children.Sum(c => c.GetSize());  // Récursif !

    public void Display(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent)}📁 {Name}/");
        foreach (var child in _children)
            child.Display(indent + 2);  // Récursif !
    }
}

// Utilisation
var root = new Folder("Projet");
var src = new Folder("src");
src.Add(new File("main.cs", 150));
src.Add(new File("utils.cs", 80));
root.Add(src);
root.Add(new File("README.md", 10));

root.Display();
// 📁 Projet/
//   📁 src/
//     📄 main.cs (150 Ko)
//     📄 utils.cs (80 Ko)
//   📄 README.md (10 Ko)

Console.WriteLine($"Taille totale: {root.GetSize()} Ko");  // 240 Ko
```

---

## 2.4 Decorator

### L'analogie
☕ **Café personnalisé** : Tu commences avec un café simple, puis tu ajoutes des "couches" : lait, sucre, chantilly. Chaque ajout "décore" le café de base.

### Le problème
Tu veux ajouter des **fonctionnalités dynamiquement** sans modifier la classe originale.

### La solution
```
┌─────────────────┐
│   Component     │◄─────────────────────────────┐
│   (interface)   │                              │
│ + Operation()   │                              │
└────────▲────────┘                              │
         │                                       │
    ┌────┴────┐                                  │
    │         │                                  │
┌───┴───┐ ┌───┴───────────┐                     │
│Concrete│ │   Decorator   │─────────────────────┘
│Component│ │ (abstract)    │  contient un Component
└────────┘ │ - component    │
           │ + Operation()  │ ──► component.Operation()
           └───────▲────────┘      + comportement additionnel
                   │
         ┌─────────┴─────────┐
         │                   │
   ┌─────┴─────┐      ┌─────┴─────┐
   │DecoratorA │      │DecoratorB │
   └───────────┘      └───────────┘
```

### Le code
```csharp
// Component
public interface ICoffee
{
    string GetDescription();
    decimal GetCost();
}

// Concrete Component
public class SimpleCoffee : ICoffee
{
    public string GetDescription() => "Café";
    public decimal GetCost() => 2.00m;
}

// Decorator abstrait
public abstract class CoffeeDecorator : ICoffee
{
    protected ICoffee _coffee;

    protected CoffeeDecorator(ICoffee coffee) => _coffee = coffee;

    public virtual string GetDescription() => _coffee.GetDescription();
    public virtual decimal GetCost() => _coffee.GetCost();
}

// Decorators concrets
public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + " + Lait";
    public override decimal GetCost() => _coffee.GetCost() + 0.50m;
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + " + Sucre";
    public override decimal GetCost() => _coffee.GetCost() + 0.20m;
}

public class WhippedCreamDecorator : CoffeeDecorator
{
    public WhippedCreamDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + " + Chantilly";
    public override decimal GetCost() => _coffee.GetCost() + 0.80m;
}

// Utilisation : on empile les décorateurs
ICoffee myCoffee = new SimpleCoffee();                    // Café - 2.00€
myCoffee = new MilkDecorator(myCoffee);                   // + Lait - 2.50€
myCoffee = new SugarDecorator(myCoffee);                  // + Sucre - 2.70€
myCoffee = new WhippedCreamDecorator(myCoffee);           // + Chantilly - 3.50€

Console.WriteLine($"{myCoffee.GetDescription()} = {myCoffee.GetCost()}€");
// Café + Lait + Sucre + Chantilly = 3.50€
```

---

## 2.5 Facade

### L'analogie
🎬 **La télécommande universelle** : Au lieu de manipuler séparément la TV, le lecteur DVD, le système audio... tu as UN seul bouton "regarder un film" qui fait tout.

### Le problème
Un système complexe avec plein de sous-systèmes. Tu veux une **interface simplifiée**.

### La solution
```
┌─────────────────────────────────────────────────────────────┐
│                         FACADE                              │
│                                                             │
│  + WatchMovie()                                             │
│  + EndMovie()                                               │
│                                                             │
└──────────────────────────┬──────────────────────────────────┘
                           │ utilise
          ┌────────────────┼────────────────┐
          ▼                ▼                ▼
   ┌─────────────┐  ┌─────────────┐  ┌─────────────┐
   │     TV      │  │  DVDPlayer  │  │   Sound     │
   │ TurnOn()    │  │ Play()      │  │ SetVolume() │
   │ SetInput()  │  │ Stop()      │  │ SetMode()   │
   └─────────────┘  └─────────────┘  └─────────────┘
```

### Le code
```csharp
// Sous-systèmes complexes
public class TV
{
    public void TurnOn() => Console.WriteLine("TV allumée");
    public void SetInput(string input) => Console.WriteLine($"TV sur {input}");
}

public class DVDPlayer
{
    public void TurnOn() => Console.WriteLine("Lecteur DVD allumé");
    public void Play(string movie) => Console.WriteLine($"Lecture de {movie}");
    public void Stop() => Console.WriteLine("Arrêt DVD");
}

public class SoundSystem
{
    public void TurnOn() => Console.WriteLine("Système audio allumé");
    public void SetVolume(int level) => Console.WriteLine($"Volume à {level}");
    public void SetMode(string mode) => Console.WriteLine($"Mode {mode}");
}

public class Lights
{
    public void Dim(int level) => Console.WriteLine($"Lumières à {level}%");
}

// FACADE : simplifie tout
public class HomeTheaterFacade
{
    private TV _tv;
    private DVDPlayer _dvd;
    private SoundSystem _sound;
    private Lights _lights;

    public HomeTheaterFacade()
    {
        _tv = new TV();
        _dvd = new DVDPlayer();
        _sound = new SoundSystem();
        _lights = new Lights();
    }

    // UNE méthode qui fait tout
    public void WatchMovie(string movie)
    {
        Console.WriteLine("=== Préparation du film ===");
        _lights.Dim(10);
        _tv.TurnOn();
        _tv.SetInput("DVD");
        _sound.TurnOn();
        _sound.SetMode("Surround");
        _sound.SetVolume(50);
        _dvd.TurnOn();
        _dvd.Play(movie);
    }

    public void EndMovie()
    {
        Console.WriteLine("=== Fin du film ===");
        _dvd.Stop();
        _lights.Dim(100);
    }
}

// Utilisation : SIMPLE !
var homeTheater = new HomeTheaterFacade();
homeTheater.WatchMovie("Inception");
// ... plus tard
homeTheater.EndMovie();
```

---

## 2.6 Proxy

### L'analogie
💳 **La carte bancaire** : Tu ne te balades pas avec 10 000€ en liquide. Ta carte est un "proxy" vers ton compte bancaire. Elle peut aussi ajouter des contrôles (code PIN, plafond).

### Le problème
Tu veux **contrôler l'accès** à un objet (lazy loading, sécurité, cache, logging).

### Types de Proxy
- **Virtual Proxy** : Lazy loading (création différée)
- **Protection Proxy** : Contrôle d'accès
- **Remote Proxy** : Représente un objet distant
- **Cache Proxy** : Met en cache les résultats

### Le code
```csharp
// Interface commune
public interface IImage
{
    void Display();
}

// Objet réel (coûteux à charger)
public class RealImage : IImage
{
    private string _filename;

    public RealImage(string filename)
    {
        _filename = filename;
        LoadFromDisk();  // Coûteux !
    }

    private void LoadFromDisk()
    {
        Console.WriteLine($"Chargement de {_filename} depuis le disque...");
        Thread.Sleep(1000);  // Simule un chargement long
    }

    public void Display() => Console.WriteLine($"Affichage de {_filename}");
}

// Proxy (lazy loading)
public class ImageProxy : IImage
{
    private string _filename;
    private RealImage? _realImage;  // Pas encore chargé

    public ImageProxy(string filename) => _filename = filename;

    public void Display()
    {
        // Charge seulement quand nécessaire !
        _realImage ??= new RealImage(_filename);
        _realImage.Display();
    }
}

// Utilisation
IImage image = new ImageProxy("photo.jpg");  // Instantané (pas de chargement)
Console.WriteLine("Image créée, mais pas encore chargée...");
// ... plus tard
image.Display();  // Maintenant ça charge et affiche
image.Display();  // Déjà chargé, affiche direct
```

---

## 2.7 Flyweight

### L'analogie
🔤 **Les caractères d'un document Word** : Dans un document de 10 000 caractères, tu n'as pas 10 000 objets "A". Tu as UN objet "A" partagé et référencé 10 000 fois.

### Le problème
Tu as **beaucoup d'objets similaires** qui consomment trop de mémoire.

### La solution
Séparer l'état **intrinsèque** (partagé) de l'état **extrinsèque** (unique).

```csharp
// Flyweight : état intrinsèque (partagé)
public class TreeType
{
    public string Name { get; }
    public string Color { get; }
    public string Texture { get; }

    public TreeType(string name, string color, string texture)
    {
        Name = name;
        Color = color;
        Texture = texture;
    }

    public void Draw(int x, int y)  // x, y = état extrinsèque
    {
        Console.WriteLine($"Arbre {Name} ({Color}) à ({x}, {y})");
    }
}

// Factory qui gère le cache des flyweights
public class TreeFactory
{
    private static Dictionary<string, TreeType> _treeTypes = new();

    public static TreeType GetTreeType(string name, string color, string texture)
    {
        string key = $"{name}_{color}_{texture}";

        if (!_treeTypes.ContainsKey(key))
        {
            _treeTypes[key] = new TreeType(name, color, texture);
            Console.WriteLine($"Nouveau type créé: {key}");
        }

        return _treeTypes[key];
    }
}

// Contexte : contient l'état extrinsèque
public class Tree
{
    private int _x, _y;
    private TreeType _type;  // Référence vers le flyweight partagé

    public Tree(int x, int y, TreeType type)
    {
        _x = x;
        _y = y;
        _type = type;
    }

    public void Draw() => _type.Draw(_x, _y);
}

// Utilisation : 1 million d'arbres mais seulement quelques TreeType
var forest = new List<Tree>();
var random = new Random();

for (int i = 0; i < 1000; i++)
{
    var type = TreeFactory.GetTreeType("Chêne", "Vert", "texture1.png");
    forest.Add(new Tree(random.Next(1000), random.Next(1000), type));
}
// Résultat : 1000 arbres mais UN SEUL TreeType en mémoire !
```

---

# 3. Patterns Comportementaux

> **But** : Gérer les algorithmes et la communication entre objets

---

## 3.1 Observer

### L'analogie
📺 **Abonnement YouTube** : Tu t'abonnes à une chaîne. Quand une nouvelle vidéo sort, tu es notifié automatiquement. Tu peux te désabonner quand tu veux.

### Le problème
Un objet change d'état et **plusieurs autres objets doivent réagir**, sans couplage fort.

### La solution
```
┌─────────────────┐         ┌─────────────────┐
│    Subject      │◄────────│    Observer     │
│                 │  0..*   │   (interface)   │
│ + Attach(obs)   │         │ + Update()      │
│ + Detach(obs)   │         └────────▲────────┘
│ + Notify()      │                  │
└─────────────────┘         ┌────────┴────────┐
                            │                 │
                     ┌──────┴──────┐   ┌──────┴──────┐
                     │ ObserverA   │   │ ObserverB   │
                     └─────────────┘   └─────────────┘
```

### Le code
```csharp
// Observer interface
public interface IObserver
{
    void Update(string message);
}

// Subject
public class NewsAgency
{
    private List<IObserver> _subscribers = new();
    private string _news = "";

    public void Subscribe(IObserver observer) => _subscribers.Add(observer);

    public void Unsubscribe(IObserver observer) => _subscribers.Remove(observer);

    public void PublishNews(string news)
    {
        _news = news;
        Notify();
    }

    private void Notify()
    {
        foreach (var observer in _subscribers)
            observer.Update(_news);
    }
}

// Observers concrets
public class EmailSubscriber : IObserver
{
    private string _email;
    public EmailSubscriber(string email) => _email = email;

    public void Update(string message)
        => Console.WriteLine($"Email à {_email}: {message}");
}

public class PhoneSubscriber : IObserver
{
    private string _phone;
    public PhoneSubscriber(string phone) => _phone = phone;

    public void Update(string message)
        => Console.WriteLine($"SMS à {_phone}: {message}");
}

// Utilisation
var agency = new NewsAgency();
var email1 = new EmailSubscriber("alice@mail.com");
var phone1 = new PhoneSubscriber("0612345678");

agency.Subscribe(email1);
agency.Subscribe(phone1);

agency.PublishNews("Breaking: Design Patterns sont géniaux !");
// Email à alice@mail.com: Breaking: Design Patterns sont géniaux !
// SMS à 0612345678: Breaking: Design Patterns sont géniaux !
```

---

## 3.2 Strategy

### L'analogie
🗺️ **GPS avec plusieurs itinéraires** : Tu choisis ta stratégie : le plus rapide, le plus court, éviter les péages. Le GPS utilise l'algorithme que tu as choisi.

### Le problème
Tu as plusieurs **algorithmes interchangeables** pour une même tâche.

### La solution
```
┌─────────────────┐         ┌─────────────────┐
│    Context      │────────►│    Strategy     │
│                 │         │   (interface)   │
│ - strategy      │         │ + Execute()     │
│ + SetStrategy() │         └────────▲────────┘
│ + DoSomething() │                  │
└─────────────────┘         ┌────────┴────────┐
                            │                 │
                     ┌──────┴──────┐   ┌──────┴──────┐
                     │ StrategyA   │   │ StrategyB   │
                     └─────────────┘   └─────────────┘
```

### Le code
```csharp
// Strategy interface
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

// Strategies concrètes
public class CreditCardPayment : IPaymentStrategy
{
    private string _cardNumber;
    public CreditCardPayment(string cardNumber) => _cardNumber = cardNumber;

    public void Pay(decimal amount)
        => Console.WriteLine($"Paiement de {amount}€ par CB {_cardNumber[^4..]}");
}

public class PayPalPayment : IPaymentStrategy
{
    private string _email;
    public PayPalPayment(string email) => _email = email;

    public void Pay(decimal amount)
        => Console.WriteLine($"Paiement de {amount}€ via PayPal ({_email})");
}

public class CryptoPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
        => Console.WriteLine($"Paiement de {amount}€ en Bitcoin");
}

// Context
public class ShoppingCart
{
    private IPaymentStrategy? _paymentStrategy;

    public void SetPaymentMethod(IPaymentStrategy strategy)
        => _paymentStrategy = strategy;

    public void Checkout(decimal total)
    {
        if (_paymentStrategy == null)
            throw new InvalidOperationException("Choisissez un mode de paiement");

        _paymentStrategy.Pay(total);
    }
}

// Utilisation
var cart = new ShoppingCart();

// Le client choisit sa stratégie
cart.SetPaymentMethod(new CreditCardPayment("1234567890123456"));
cart.Checkout(99.99m);

// Il peut changer d'avis
cart.SetPaymentMethod(new PayPalPayment("user@mail.com"));
cart.Checkout(49.99m);
```

---

## 3.3 Command

### L'analogie
🎮 **Boutons de télécommande** : Chaque bouton encapsule une action. Tu peux aussi défaire (Undo) tes actions.

### Le problème
Tu veux **encapsuler une requête** comme un objet, permettre l'annulation, la mise en file d'attente, le logging.

### La solution
```
┌─────────────┐     ┌─────────────┐     ┌─────────────┐
│   Invoker   │────►│   Command   │────►│  Receiver   │
│             │     │ (interface) │     │             │
│ + Execute() │     │ + Execute() │     │ + Action()  │
│             │     │ + Undo()    │     │             │
└─────────────┘     └─────────────┘     └─────────────┘
```

### Le code
```csharp
// Receiver
public class Light
{
    public void TurnOn() => Console.WriteLine("💡 Lumière allumée");
    public void TurnOff() => Console.WriteLine("🌑 Lumière éteinte");
}

// Command interface
public interface ICommand
{
    void Execute();
    void Undo();
}

// Commandes concrètes
public class LightOnCommand : ICommand
{
    private Light _light;
    public LightOnCommand(Light light) => _light = light;

    public void Execute() => _light.TurnOn();
    public void Undo() => _light.TurnOff();
}

public class LightOffCommand : ICommand
{
    private Light _light;
    public LightOffCommand(Light light) => _light = light;

    public void Execute() => _light.TurnOff();
    public void Undo() => _light.TurnOn();
}

// Invoker (télécommande)
public class RemoteControl
{
    private ICommand? _lastCommand;

    public void PressButton(ICommand command)
    {
        command.Execute();
        _lastCommand = command;
    }

    public void PressUndo()
    {
        if (_lastCommand != null)
        {
            Console.WriteLine("↩️ Undo:");
            _lastCommand.Undo();
        }
    }
}

// Utilisation
var light = new Light();
var remote = new RemoteControl();

remote.PressButton(new LightOnCommand(light));   // 💡 Lumière allumée
remote.PressUndo();                               // 🌑 Lumière éteinte
```

---

## 3.4 State

### L'analogie
🚦 **Feu de circulation** : Le feu a 3 états (vert, orange, rouge). Son comportement change selon l'état actuel.

### Le problème
Un objet change de **comportement selon son état** interne (éviter les gros switch/if).

### Le code
```csharp
// State interface
public interface IState
{
    void Handle(Context context);
}

// Context
public class Context
{
    public IState State { get; set; }

    public Context(IState initialState) => State = initialState;

    public void Request() => State.Handle(this);
}

// États concrets
public class GreenState : IState
{
    public void Handle(Context context)
    {
        Console.WriteLine("🟢 Vert → Les voitures passent");
        context.State = new OrangeState();  // Transition
    }
}

public class OrangeState : IState
{
    public void Handle(Context context)
    {
        Console.WriteLine("🟠 Orange → Attention, ralentir");
        context.State = new RedState();
    }
}

public class RedState : IState
{
    public void Handle(Context context)
    {
        Console.WriteLine("🔴 Rouge → Stop !");
        context.State = new GreenState();
    }
}

// Utilisation
var trafficLight = new Context(new GreenState());
trafficLight.Request();  // 🟢 Vert → Les voitures passent
trafficLight.Request();  // 🟠 Orange → Attention, ralentir
trafficLight.Request();  // 🔴 Rouge → Stop !
trafficLight.Request();  // 🟢 Vert → Les voitures passent
```

---

## 3.5 Template Method

### L'analogie
🍳 **Recette de cuisine** : Les étapes sont définies (préparer, cuire, servir), mais chaque chef peut personnaliser certaines étapes.

### Le problème
Tu as un **algorithme avec des étapes fixes**, mais certaines étapes varient selon les sous-classes.

### Le code
```csharp
// Template (classe abstraite)
public abstract class DataMiner
{
    // Template Method : définit le squelette
    public void Mine(string path)
    {
        OpenFile(path);
        ExtractData();
        ParseData();
        AnalyzeData();
        SendReport();
        CloseFile();
    }

    // Étapes communes
    private void AnalyzeData() => Console.WriteLine("Analyse des données...");
    private void SendReport() => Console.WriteLine("Envoi du rapport...");

    // Étapes à implémenter par les sous-classes
    protected abstract void OpenFile(string path);
    protected abstract void ExtractData();
    protected abstract void ParseData();
    protected abstract void CloseFile();
}

// Implémentations concrètes
public class CsvDataMiner : DataMiner
{
    protected override void OpenFile(string path)
        => Console.WriteLine($"Ouverture CSV: {path}");
    protected override void ExtractData()
        => Console.WriteLine("Extraction lignes CSV...");
    protected override void ParseData()
        => Console.WriteLine("Parsing colonnes CSV...");
    protected override void CloseFile()
        => Console.WriteLine("Fermeture fichier CSV");
}

public class PdfDataMiner : DataMiner
{
    protected override void OpenFile(string path)
        => Console.WriteLine($"Ouverture PDF: {path}");
    protected override void ExtractData()
        => Console.WriteLine("Extraction texte PDF...");
    protected override void ParseData()
        => Console.WriteLine("Parsing contenu PDF...");
    protected override void CloseFile()
        => Console.WriteLine("Fermeture document PDF");
}

// Utilisation
DataMiner miner = new CsvDataMiner();
miner.Mine("data.csv");
```

---

## 3.6 Iterator

### L'analogie
🎵 **Playlist musicale** : Tu parcours les chansons une par une (suivant, précédent) sans savoir comment elles sont stockées (tableau, liste, arbre...).

### Le code
```csharp
// En C#, on utilise IEnumerable<T> et IEnumerator<T>
public class Playlist : IEnumerable<string>
{
    private List<string> _songs = new();

    public void AddSong(string song) => _songs.Add(song);

    public IEnumerator<string> GetEnumerator()
    {
        foreach (var song in _songs)
            yield return song;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// Utilisation
var playlist = new Playlist();
playlist.AddSong("Bohemian Rhapsody");
playlist.AddSong("Hotel California");
playlist.AddSong("Stairway to Heaven");

foreach (var song in playlist)  // Utilise l'iterator
    Console.WriteLine($"🎵 {song}");
```

---

## 3.7 Mediator

### L'analogie
✈️ **Tour de contrôle aérien** : Les avions ne communiquent pas directement entre eux. Ils passent par la tour de contrôle qui coordonne tout.

### Le problème
Trop de **communications directes** entre objets (couplage fort).

### Le code
```csharp
// Mediator
public class ChatRoom
{
    public void ShowMessage(User user, string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm}] {user.Name}: {message}");
    }
}

// Colleague
public class User
{
    public string Name { get; }
    private ChatRoom _chatRoom;

    public User(string name, ChatRoom chatRoom)
    {
        Name = name;
        _chatRoom = chatRoom;
    }

    public void Send(string message) => _chatRoom.ShowMessage(this, message);
}

// Utilisation
var chatRoom = new ChatRoom();
var alice = new User("Alice", chatRoom);
var bob = new User("Bob", chatRoom);

alice.Send("Salut Bob !");
bob.Send("Hey Alice !");
```

---

## 3.8 Chain of Responsibility

### L'analogie
🏢 **Demande de congés** : Tu demandes à ton chef. S'il ne peut pas décider, il passe au directeur. S'il ne peut pas, ça monte au PDG.

### Le code
```csharp
// Handler
public abstract class Approver
{
    protected Approver? _nextApprover;

    public void SetNext(Approver next) => _nextApprover = next;

    public abstract void ProcessRequest(int amount);
}

// Handlers concrets
public class Manager : Approver
{
    public override void ProcessRequest(int amount)
    {
        if (amount <= 1000)
            Console.WriteLine($"Manager approuve {amount}€");
        else
            _nextApprover?.ProcessRequest(amount);
    }
}

public class Director : Approver
{
    public override void ProcessRequest(int amount)
    {
        if (amount <= 10000)
            Console.WriteLine($"Directeur approuve {amount}€");
        else
            _nextApprover?.ProcessRequest(amount);
    }
}

public class CEO : Approver
{
    public override void ProcessRequest(int amount)
    {
        Console.WriteLine($"PDG approuve {amount}€");
    }
}

// Utilisation
var manager = new Manager();
var director = new Director();
var ceo = new CEO();

manager.SetNext(director);
director.SetNext(ceo);

manager.ProcessRequest(500);    // Manager approuve
manager.ProcessRequest(5000);   // Directeur approuve
manager.ProcessRequest(50000);  // PDG approuve
```

---

## 3.9 Visitor

### L'analogie
🏥 **Médecin qui visite des patients** : Le médecin (visitor) va voir chaque patient. Selon le type de patient (enfant, adulte, senior), il adapte son traitement.

### Quand l'utiliser ?
- Tu as une structure d'objets stable
- Tu veux ajouter des opérations sans modifier les classes

---

## 3.10 Memento

### L'analogie
💾 **Sauvegarde de jeu vidéo** : Tu peux sauvegarder l'état du jeu et y revenir plus tard.

### Le code
```csharp
// Memento
public class EditorMemento
{
    public string Content { get; }
    public EditorMemento(string content) => Content = content;
}

// Originator
public class TextEditor
{
    public string Content { get; set; } = "";

    public EditorMemento Save() => new EditorMemento(Content);
    public void Restore(EditorMemento memento) => Content = memento.Content;
}

// Caretaker
public class History
{
    private Stack<EditorMemento> _mementos = new();

    public void Push(EditorMemento memento) => _mementos.Push(memento);
    public EditorMemento Pop() => _mementos.Pop();
}

// Utilisation
var editor = new TextEditor();
var history = new History();

editor.Content = "Hello";
history.Push(editor.Save());

editor.Content = "Hello World";
history.Push(editor.Save());

editor.Content = "Hello World !!!";

editor.Restore(history.Pop());  // Retour à "Hello World"
editor.Restore(history.Pop());  // Retour à "Hello"
```

---

# Cheat Sheet

## Quand utiliser quel pattern ?

| Besoin | Pattern | Catégorie |
|--------|---------|-----------|
| Une seule instance | **Singleton** | Créationnel |
| Créer sans connaître la classe | **Factory Method** | Créationnel |
| Familles d'objets cohérents | **Abstract Factory** | Créationnel |
| Construction étape par étape | **Builder** | Créationnel |
| Copier un objet existant | **Prototype** | Créationnel |
| Interfaces incompatibles | **Adapter** | Structurel |
| Séparer abstraction/implémentation | **Bridge** | Structurel |
| Structures arborescentes | **Composite** | Structurel |
| Ajouter des fonctionnalités | **Decorator** | Structurel |
| Simplifier un système complexe | **Facade** | Structurel |
| Économiser la mémoire | **Flyweight** | Structurel |
| Contrôler l'accès | **Proxy** | Structurel |
| Passer une requête à une chaîne | **Chain of Responsibility** | Comportemental |
| Encapsuler une action + undo | **Command** | Comportemental |
| Parcourir une collection | **Iterator** | Comportemental |
| Réduire le couplage | **Mediator** | Comportemental |
| Sauvegarder/restaurer un état | **Memento** | Comportemental |
| Notifier plusieurs objets | **Observer** | Comportemental |
| Comportement selon l'état | **State** | Comportemental |
| Changer d'algorithme | **Strategy** | Comportemental |
| Squelette d'algorithme | **Template Method** | Comportemental |
| Opérations sur structure stable | **Visitor** | Comportemental |

---

## Patterns similaires - Les différences

### Factory Method vs Abstract Factory
| Factory Method | Abstract Factory |
|----------------|------------------|
| Crée UN produit | Crée une FAMILLE de produits |
| Utilise l'héritage | Utilise la composition |
| Une méthode | Plusieurs méthodes |

### Adapter vs Bridge vs Decorator
| Adapter | Bridge | Decorator |
|---------|--------|-----------|
| Fait fonctionner des interfaces incompatibles | Sépare abstraction et implémentation | Ajoute des fonctionnalités |
| Après coup (legacy) | Dès la conception | Dynamiquement |
| Traduit | Découple | Empile |

### Strategy vs State
| Strategy | State |
|----------|-------|
| Le CLIENT choisit l'algorithme | L'OBJET change son comportement |
| Algorithmes interchangeables | Transitions entre états |
| Pas de lien entre stratégies | Les états se connaissent |

### Observer vs Mediator
| Observer | Mediator |
|----------|----------|
| 1 sujet → N observateurs | N objets ↔ 1 médiateur |
| Notification broadcast | Communication centralisée |
| Couplage faible | Découplage total |

---

## Les principes SOLID rappelés

| Principe | Description | Patterns associés |
|----------|-------------|-------------------|
| **S**ingle Responsibility | Une classe = une raison de changer | Factory, Strategy |
| **O**pen/Closed | Ouvert à l'extension, fermé à la modification | Decorator, Adapter, Strategy |
| **L**iskov Substitution | Sous-types substituables | Template Method, Factory |
| **I**nterface Segregation | Interfaces spécifiques | Adapter, Facade |
| **D**ependency Inversion | Dépendre des abstractions | Factory, Strategy, Bridge |

---

> 📝 **Conseil final** : Ne force pas l'utilisation d'un pattern. Utilise-le quand le problème correspond vraiment au pattern. Un code simple sans pattern est souvent meilleur qu'un code sur-engineered.
