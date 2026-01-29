using System;
using System.Collections.Generic;

// ============================================================
// APPROCHE 1 : Interface unifiée (transparence)
// Leaf implémente toutes les méthodes mais lève des exceptions
// ============================================================

namespace Approche1_Transparence
{
    public interface IComponent
    {
        void Operation();
        void Add(IComponent component);
        void Remove(IComponent component);
        IComponent GetChild(int index);
    }

    public class Leaf : IComponent
    {
        private string _name;

        public Leaf(string name) => _name = name;

        public void Operation() => Console.WriteLine($"  Leaf: {_name}");

        public void Add(IComponent component)
            => throw new NotSupportedException("Impossible d'ajouter à une feuille.");

        public void Remove(IComponent component)
            => throw new NotSupportedException("Impossible de retirer d'une feuille.");

        public IComponent GetChild(int index)
            => throw new NotSupportedException("Une feuille n'a pas d'enfants.");
    }

    public class Composite : IComponent
    {
        private string _name;
        private List<IComponent> _children = new List<IComponent>();

        public Composite(string name) => _name = name;

        public void Operation()
        {
            Console.WriteLine($"Composite: {_name}");
            foreach (var child in _children)
                child.Operation();
        }

        public void Add(IComponent component) => _children.Add(component);
        public void Remove(IComponent component) => _children.Remove(component);
        public IComponent GetChild(int index) => _children[index];
    }

    // Avantage  : Le client traite Leaf et Composite uniformément via IComponent
    // Inconvénient : fichier.Add(...) compile mais lève une exception à l'exécution
}

// ============================================================
// APPROCHE 2 : Interface séparée (sécurité)
// Seul Composite expose Add/Remove/GetChild
// ============================================================

namespace Approche2_Securite
{
    public interface IComponent
    {
        void Operation();
    }

    public class Leaf : IComponent
    {
        private string _name;

        public Leaf(string name) => _name = name;

        public void Operation() => Console.WriteLine($"  Leaf: {_name}");
    }

    public class Composite : IComponent
    {
        private string _name;
        private List<IComponent> _children = new List<IComponent>();

        public Composite(string name) => _name = name;

        public void Operation()
        {
            Console.WriteLine($"Composite: {_name}");
            foreach (var child in _children)
                child.Operation();
        }

        public void Add(IComponent component) => _children.Add(component);
        public void Remove(IComponent component) => _children.Remove(component);
        public IComponent GetChild(int index) => _children[index];
    }

    // Avantage  : fichier.Add(...) ne compile pas → sécurité au compile-time
    // Inconvénient : le client doit connaître le type concret pour gérer les enfants
}
