using System.Xml.Serialization;

namespace CabinetInfirmier.Csharp;


[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]

public class AdresseSerealisation
{
    [XmlElement("étage")]
    public int _Etage  { get; set; }
    /*{
        get => _Etage;
        set
        {
            if (value <= 0) _Etage = 0;
            else _Etage = value;
        }
    }*/

    [XmlElement("numéro")]
    public int _Numero  { get; set; }
    /*{
        get => _Numero;
        set
        {
            if (value <= 0) _Numero = 0;
            else _Numero = value;
        }
    }*/


    [XmlElement("rue")]
    public string _Rue { get; set; }
    
    [XmlElement("codePostal")]
    public string _CodePostal { get; set; }
    
    [XmlElement("ville")]
    public string _Ville { get; set; }
    
    public AdresseSerealisation() { }

    public AdresseSerealisation(int etage, int numero, string rue, string codePostal, string ville)
    {
        _Numero = numero;
        _Rue = rue;
        _CodePostal = codePostal;
        _Ville = ville;
        _Etage = etage;
    }
}