// Pattern Builder - Construction étape par étape
public class VehiculeBuilder
{
    private Vehicule _vehicule = new Vehicule();

    // Chaque méthode retourne "this" pour permettre le chaînage
    public VehiculeBuilder SetMarque(string marque)
    {
        _vehicule.Marque = marque;
        return this;
    }

    public VehiculeBuilder SetModele(string modele)
    {
        _vehicule.Modele = modele;
        return this;
    }

    public VehiculeBuilder SetCouleur(string couleur)
    {
        _vehicule.Couleur = couleur;
        return this;
    }

    public VehiculeBuilder SetPuissance(int cv)
    {
        _vehicule.Puissance = cv;
        return this;
    }

    public VehiculeBuilder AvecGPS()
    {
        _vehicule.GPS = true;
        return this;
    }

    public VehiculeBuilder AvecClimatisation()
    {
        _vehicule.Climatisation = true;
        return this;
    }

    public VehiculeBuilder AvecToitOuvrant()
    {
        _vehicule.ToitOuvrant = true;
        return this;
    }

    public VehiculeBuilder SetPrix(decimal prix)
    {
        _vehicule.Prix = prix;
        return this;
    }

    // Méthode finale qui retourne l'objet construit
    public Vehicule Build()
    {
        Vehicule resultat = _vehicule;
        _vehicule = new Vehicule(); // Reset pour réutilisation
        return resultat;
    }
}
