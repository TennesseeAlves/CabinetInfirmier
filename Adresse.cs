namespace CabinetInfirmier.Csharp;

public class Adresse
{
    private string nom;
    private int etage;
    private int numero;
    private string rue;
    private int codePostal;
    private string ville;
    

    public Adresse(string nom, int etage, int numero, string rue, int codePostal, string ville)
    {
        this.nom = nom;
        if (etage != 0)
        {
            this.etage = etage;
        }
        if (numero != 0)
        {
            this.numero = numero;
        }
        this.rue = rue;
        this.codePostal = codePostal;
        this.ville = ville;
    }

    public string getNom()
    {
        return this.nom;
    }

    public int getEtage()
    {
        return this.etage;
    }

    public int getNumero()
    {
        return this.numero;
    }

    public string getRue()
    {
        return this.rue;
    }

    public int getCodePostal()
    {
        return this.codePostal;
    }

    public string getVille()
    {
        return this.ville;
    }
    
    
}