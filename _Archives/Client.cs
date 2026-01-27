// Classe abstraite Client (Creator)
public abstract class Client
{
    public string Nom { get; set; }

    public Client(string nom)
    {
        Nom = nom;
    }

    // Factory Method - méthode abstraite
    public abstract Commande CreeCommande();
}

// Client qui paye comptant
public class ClientComptant : Client
{
    public ClientComptant(string nom) : base(nom) { }

    // Implémentation de la Factory Method
    public override Commande CreeCommande()
    {
        return new CommandeComptant();
    }
}

// Client qui paye à crédit
public class ClientCredit : Client
{
    public ClientCredit(string nom) : base(nom) { }

    // Implémentation de la Factory Method
    public override Commande CreeCommande()
    {
        return new CommandeCredit();
    }
}
