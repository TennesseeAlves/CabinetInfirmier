namespace CabinetInfirmier;

using System.Text.RegularExpressions;
using System.Xml.Serialization;

[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]

public class AdresseSerialisation
{
    [XmlIgnore]
    private String patternEntierPositif = @"^0*[1-9]+$";
    private String patternCodePostale =  @"^\d{5}$";
    
    [XmlElement("étage", IsNullable = true)]
    public int? Etage
    {
        get { return Etage; }
        set
        {
            if (Regex.IsMatch(value.ToString(), patternEntierPositif))
                Numero = value;
            else
                throw new Exception("Un etage d'adresse doit etre un entier positif.");
        }
    }
    
    [XmlElement("numéro")]
    public int? Numero
    {
        get { return Numero; }
        set
        {
            if (Regex.IsMatch(value.ToString(), patternEntierPositif))
                Numero = value;
            else
                throw new Exception("Un numero d'adresse doit etre un entier positif.");
        }
    }
    /*{
        get => Numero;
        set
        {
            if (value <= 0) Numero = 0;
            else Numero = value;
        }
    }*/


    [XmlElement("rue")]
    public string Rue { get; set; }
    
    [XmlElement("codePostal")]
    public string CodePostal
    {
        get
        {
            return CodePostal;
        }   
        set
        {
            if (Regex.IsMatch(value.ToString(), patternCodePostale))
                CodePostal = value;
            else
                throw new Exception("Un code postale d'adresse doit etre un entier à 5 chiffres.");
        }
    }
    
    [XmlElement("ville")]
    public string Ville { get; set; }
    
    public AdresseSerialisation() { }

    public AdresseSerialisation(int? etage, int? numero, string rue, string codePostal, string ville)
    {
        Numero = numero;
        Rue = rue;
        CodePostal = codePostal;
        Ville = ville;
        Etage = etage;
    }

    
}