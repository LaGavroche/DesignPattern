using System;

// Composant de base
public abstract class Boisson
{
    public abstract string GetDescription();
    public abstract double GetCost();
}

// Composant concret
public class Coffee : Boisson
{
    public override string GetDescription() => "Café simple";
    public override double GetCost() => 2.0;
}

// Décorateur abstrait
public abstract class BoissonDecorator : Boisson
{
    protected Boisson _boisson;

    protected BoissonDecorator(Boisson boisson)
    {
        _boisson = boisson;
    }
}

// Décorateurs concrets
public class MilkDecorator : BoissonDecorator
{
    public MilkDecorator(Boisson boisson) : base(boisson) { }

    public override string GetDescription() => _boisson.GetDescription() + ", Lait";
    public override double GetCost() => _boisson.GetCost() + 0.5;
}

public class SugarDecorator : BoissonDecorator
{
    public SugarDecorator(Boisson boisson) : base(boisson) { }

    public override string GetDescription() => _boisson.GetDescription() + ", Sucre";
    public override double GetCost() => _boisson.GetCost() + 0.2;
}

public class CaramelDecorator : BoissonDecorator
{
    public CaramelDecorator(Boisson boisson) : base(boisson) { }

    public override string GetDescription() => _boisson.GetDescription() + ", Caramel";
    public override double GetCost() => _boisson.GetCost() + 0.8;
}

// Utilisation
class Program
{
    static void Main()
    {
        // Café simple
        Boisson cafe = new Coffee();
        Console.WriteLine($"{cafe.GetDescription()} : {cafe.GetCost()}€");
        // Café simple : 2€

        // Café avec lait, sucre ET caramel
        cafe = new MilkDecorator(cafe);
        cafe = new SugarDecorator(cafe);
        cafe = new CaramelDecorator(cafe);
        Console.WriteLine($"{cafe.GetDescription()} : {cafe.GetCost()}€");
        // Café simple, Lait, Sucre, Caramel : 3.5€
    }
}
