// Classe abstraite Commande (Produit)
public abstract class Commande
{
    public abstract void Afficher();
}

// Commande pour paiement comptant
public class CommandeComptant : Commande
{
    public override void Afficher()
    {
        Console.WriteLine("Commande avec paiement COMPTANT");
    }
}

// Commande pour paiement à crédit
public class CommandeCredit : Commande
{
    public override void Afficher()
    {
        Console.WriteLine("Commande avec paiement à CRÉDIT");
    }
}
