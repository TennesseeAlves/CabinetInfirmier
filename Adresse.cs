namespace CabinetInfirmier;

using System.Text.RegularExpressions;
using System.Xml.Serialization;

[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]

public class Adresse
{
    [XmlIgnore]
    private String patternEntierPositif = @"^0*[1-9]+$";
    private String patternCodePostale =  @"^\d{5}$";
    
    [XmlElement("étage", IsNullable = true)]
    public int Etage
    {
        get => Etage;
        set
        {
            if (Regex.IsMatch(value.ToString(), patternEntierPositif))
                Numero = value;
            else
                throw new Exception("Un etage d'adresse doit etre un entier positif.");
        }
    }
    
    [XmlElement("numéro")]
    public int Numero
    {
        get => Numero;
        set
        {
            if (Regex.IsMatch(value.ToString(), patternEntierPositif))
                Numero = value;
            else
                throw new Exception("Un numero d'adresse doit etre un entier positif.");
        }
    }

    [XmlElement("rue")]
    public string Rue { get; set; }
    
    [XmlElement("codePostal")]
    public string CodePostal
    {
        get => CodePostal;
        set
        {
            if (Regex.IsMatch(value, patternCodePostale))
                CodePostal = value;
            else
                throw new Exception("Un code postale d'adresse doit etre un entier à 5 chiffres.");
        }
    }
    
    [XmlElement("ville")]
    public string Ville { get; set; }
    
    public Adresse() { }

    public Adresse(int etage, int numero, string rue, string codePostal, string ville)
    {
        Numero = numero;
        Rue = rue;
        CodePostal = codePostal;
        Ville = ville;
        Etage = etage;
    }
    public string toString()
    {
        string res = "Adresse : Etage : " + Etage + "\nNumero : " + Numero + "\nrue : " + Rue + "\ncodePostal : "  + CodePostal + "\nville : " + Ville; 
        return res;
    }
}

